using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure.SP_Usuario_Aplicacion;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Listado_usuario_app
{
    public class ListadoUsuarioAplicacionHandler : IRequestHandler<ListadoUsuarioAplicacionVM, Iresult>
    {
        private readonly IRepository<Usuario_Aplicacion> _repositoryUsuarioApp;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ListadoUsuarioAplicacionHandler(
            IRepository<Usuario_Aplicacion> usuarioAppRepository,
            IRepository<Usuario> usuarioRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Menú> menuRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _repositoryUsuarioApp = usuarioAppRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryMenu = menuRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///  obtener los valores desde el procedimiento almacenado USP_SEL_USER_APP
        ///  de acuerdo a los parametros enviados
        /// </summary>
        public async Task<Iresult> Handle(ListadoUsuarioAplicacionVM request, CancellationToken cancellationToken)
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

            var menus = _repositoryAppRolMenu.TableNoTracking.Select(x => x.I_ID_MENU).ToList();

            var cant_menus = menus.Distinct().Count();

            object[] parameters = {
                request.I_PAGE_NUMBER,
                request.I_PAGE_SIZE,
                request.V_FILTER_TYPE,
                request.V_FILTER_VALUE,
                request.I_SORT_BY_FIELD,
                request.V_SORT_ORDER,
                cant_menus
            };

            var sp_listado_usuario_app = await _unitOfWork.ExcuteStoreQueryList<entity_Listado_usuario_app>("[seguridad].[USP_SEL_USER_APP] {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} OUTPUT", parameters);
            var map = _mapper.Map<List<ListadoUsuarioAplicacionDTO>>(sp_listado_usuario_app.Item1);
            var Grid = new ResultGrid<List<ListadoUsuarioAplicacionDTO>>
            {
                Total_paginas = Convert.ToInt32(request.I_PAGE_SIZE),
                Total_registros = sp_listado_usuario_app.Item2,
                data = map
            };

            if (Grid != null)
            {
                return new SuccessResult<ResultGrid<List<ListadoUsuarioAplicacionDTO>>>(Grid);
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
