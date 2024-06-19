using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure.SP_Persona;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Persona_.Queries.ListadoPersonas
{
    public class ListadoPersonasHandler : IRequestHandler<ListadoPersonasVM, Iresult>
    {
        private readonly IRepository<Persona> _repositoryPersonal;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccesor;

        public ListadoPersonasHandler(
            IRepository<Persona> personalRepository,
            IRepository<Usuario> usuarioRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccesor)
        {
            _repositoryPersonal = personalRepository;
            _repositoryUsuario = usuarioRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccesor = httpContextAccesor;
        }

        /// <summary>
        ///  obtener los valores desde el procedimiento almacenado USP_SEL_LISTADO_PERSONA
        ///  de acuerdo a los parametros enviados
        /// </summary>
        public async Task<Iresult> Handle(ListadoPersonasVM request, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccesor?.HttpContext?.User?.Claims;
            var claimUserId = claims?.FirstOrDefault(c => c.Type == "IDUser")?.Value;

            var usuario = _repositoryUsuario.TableNoTracking
                .Where(x => x.B_ESTADO == "1" && x.I_ID_USUARIO == Convert.ToInt32(claimUserId)).FirstOrDefault();

            if (usuario is null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("05", "Usuario no autorizado")
                    }
                };
            }

            request.I_PAGE_NUMBER ??= 1;
            request.I_PAGE_SIZE ??= 10;
            
            object[] parameters = {
                request.I_PAGE_NUMBER,
                request.I_PAGE_SIZE,
                request.V_FILTER_TYPE,
                request.V_FILTER_VALUE,
                request.I_SORT_BY_FIELD,
                request.V_SORT_ORDER,
            };
            
            var sp_listado_persona = await _unitOfWork.ExcuteStoreQueryList<entity_Listado_persona>("[dbo].[USP_SEL_LISTADO_PERSONA] {0}, {1}, {2}, {3}, {4}, {5}, {6} OUTPUT", parameters);
            var map = _mapper.Map<List<ListadoPersonasDTO>>(sp_listado_persona.Item1);
            var Grid = new ResultGrid<List<ListadoPersonasDTO>>
            {
                Total_paginas = Convert.ToInt32(request.I_PAGE_SIZE),
                Total_registros = sp_listado_persona.Item2,
                data = map
            };

            if (Grid != null)
            {
                return new SuccessResult<ResultGrid<List<ListadoPersonasDTO>>>(Grid);
            }
            else
            {
                return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 500,
                Value = new List<DetailError>()
                    {
                        new DetailError("01", "No se pudo obtener respuesta.")
                    }
            };
            }
        }
    }
}
