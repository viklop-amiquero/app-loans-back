using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure.SP_Aplicacion;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Application.Listado_Aplicaciones
{
    public class ListadoAplicacionesHandler : IRequestHandler <ListadoAplicacionesVM , Iresult>
    {
        private readonly IRepository<Aplicacion> _repositoryAplicacion;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListadoAplicacionesHandler(
            IRepository<Aplicacion> aplicacionRepository,
            IRepository<Usuario> usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repositoryAplicacion = aplicacionRepository;
            _repositoryUsuario = usuarioRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  obtener los valores desde el procedimiento almacenado UPS_SEL_APLICACION
        ///  de acuerdo a los parametros enviados
        /// </summary>


        public async Task<Iresult> Handle(ListadoAplicacionesVM request, CancellationToken cancellationToken)
        {
            var identity = _httpContextAccessor as ClaimContextAccessor;
            var usuario = _repositoryUsuario.Table.FirstOrDefault(x => x.V_USUARIO == Convert.ToString(identity.UserName));

            if (usuario == null)
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

            var sp_listado_aplicacion = await _unitOfWork.ExcuteStoreQueryList<entity_Listado_aplicacion>("[seguridad].[UPS_SEL_APLICACION] {0}, {1}, {2}, {3}, {4}, {5}, {6} OUTPUT", parameters);
            var map = _mapper.Map<List<ListadoAplicacionesDTO>>(sp_listado_aplicacion.Item1);
            var Grid = new ResultGrid<List<ListadoAplicacionesDTO>>
            {
                Total_paginas = Convert.ToInt32(request.I_PAGE_SIZE),
                Total_registros = sp_listado_aplicacion.Item2,
                data = map
            };

            if (Grid != null)
            {
                return new SuccessResult<ResultGrid<List<ListadoAplicacionesDTO>>>(Grid);
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
