using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure.SP_Operacion;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Operacion_.Queries.Listado_operaciones
{
    public class ListadoOperacionesHandler : IRequestHandler<ListadoOperacionesVM, Iresult>
    {
        private readonly IRepository<Operacion> _repositoryOperacion;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ListadoOperacionesHandler(
            IRepository<Operacion> operacionRepository,
            IRepository<Usuario> usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repositoryOperacion = operacionRepository;
            _repositoryUsuario = usuarioRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


        public async Task<Iresult> Handle(ListadoOperacionesVM request, CancellationToken cancellationToken)
        {
            //var identity = _httpContextAccessor as ClaimContextAccessor;
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
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
                request.I_SORT_BY_FIELD??3,
                request.V_SORT_ORDER??"DESC",
            };

            var sp_listado_operacion = await _unitOfWork.ExcuteStoreQueryList<entity_listado_operacion>("[rapidiario].[USP_SEL_OPERACION] {0}, {1}, {2}, {3}, {4}, {5}, {6} OUTPUT", parameters);
            var map = _mapper.Map<List<ListadoOperacionesDTO>>(sp_listado_operacion.Item1);
            var Grid = new ResultGrid<List<ListadoOperacionesDTO>>
            {
                Total_paginas = Convert.ToInt32(request.I_PAGE_SIZE),
                Total_registros = sp_listado_operacion.Item2,
                data = map
            };

            if (Grid != null)
            {
                return new SuccessResult<ResultGrid<List<ListadoOperacionesDTO>>>(Grid);
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
