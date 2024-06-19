using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure.SP_AplicacionRolMenu;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_rol_menu_.Listado_app_rol_menu
{
    public class ListadoAplicacionRolMenuHandler : IRequestHandler<ListadoAplicacionRolMenuVM, Iresult>
    {
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ListadoAplicacionRolMenuHandler(
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Usuario> usuarioRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryUsuario = usuarioRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  obtener los valores desde el procedimiento almacenado USP_SEL_APP_ROL_MENU
        ///  de acuerdo a los parametros enviados
        /// </summary>
        public async Task<Iresult> Handle(ListadoAplicacionRolMenuVM request, CancellationToken cancellationToken)
        {
            var Claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var claimUserId = Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

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

            var sp_listado_app_rol_menu = await _unitOfWork.ExcuteStoreQueryList<entity_Listado_aplicacion_rol_menu>("[seguridad].[USP_SEL_APP_ROL_MENU] {0}, {1}, {2}, {3}, {4}, {5}, {6} OUTPUT", parameters);
            var map = _mapper.Map<List<ListadoAplicacionRolMenuDTO>>(sp_listado_app_rol_menu.Item1);
            var Grid = new ResultGrid<List<ListadoAplicacionRolMenuDTO>>
            {
                Total_paginas = Convert.ToInt32(request.I_PAGE_SIZE),
                Total_registros = sp_listado_app_rol_menu.Item2,
                data = map
            };

            if (Grid != null)
            {
                return new SuccessResult<ResultGrid<List<ListadoAplicacionRolMenuDTO>>>(Grid);
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
