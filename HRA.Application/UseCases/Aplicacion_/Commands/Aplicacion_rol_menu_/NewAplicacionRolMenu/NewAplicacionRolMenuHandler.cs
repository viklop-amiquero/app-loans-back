using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.NewAplicacionRolMenu
{
    public class NewAplicacionRolMenuHandler : IRequestHandler<NewAplicacionRolMenuVM, Iresult>
    {
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Aplicacion> _repositoryApp;
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IRepository<Rol> _repositoryRol;
        private readonly IRepository<Permiso> _repositoryPermiso;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public NewAplicacionRolMenuHandler(
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Aplicacion> appRepository,
            IRepository<Menú> menuRepository,
            IRepository<Rol> rolRepository,
            IRepository<Permiso> permisoRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryApp = appRepository;
            _repositoryMenu = menuRepository;
            _repositoryRol = rolRepository;
            _repositoryPermiso = permisoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(NewAplicacionRolMenuVM request, CancellationToken cancellationToken)
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

            //var app = _repositoryApp.TableNoTracking.FirstOrDefault(x => x.I_ID_APLICACION.ToString() == request.I_APP_ID && x.B_ESTADO == "1");
            
            //if (app == null)
            //{
            //    return new FailureResult<IEnumerable<DetailError>>()
            //    {
            //        Value = new List<DetailError>()
            //        {
            //            new DetailError("02", "No existe la aplicación o está inactiva")
            //        }
            //    };
            //}

            var menu = _repositoryMenu.TableNoTracking.FirstOrDefault(x => x.I_ID_MENU.ToString() == request.I_MENU_ID && x.B_ESTADO == "1");

            if (menu == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el menú o está inactivo o no pertenece a la aplicación")
                    }
                };
            }

            var rol = _repositoryRol.TableNoTracking.FirstOrDefault(x => x.I_ID_ROL.ToString() == request.I_ROLE_ID && x.B_ESTADO == "1");

            if (rol == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el rol o está inactivo")
                    }
                };
            }

            var permiso = _repositoryPermiso.TableNoTracking.FirstOrDefault(x => x.I_ID_PERMISO.ToString() == request.I_PERMISSION_ID && x.B_ESTADO == "1");

            if (permiso == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el permiso o está inactivo")
                    }
                };
            }

            if (_repositoryAppRolMenu.TableNoTracking.Where(
                x => x.I_ID_MENU == menu.I_ID_MENU && x.I_ID_ROL == rol.I_ID_ROL).ToList().Count == 0)
            {
                _repositoryAppRolMenu.Insert(new List<Aplicacion_Rol_Menu>
                {
                    new Aplicacion_Rol_Menu
                    {
                        I_ID_MENU = menu.I_ID_MENU,
                        I_ID_ROL = rol.I_ID_ROL,
                        I_ID_PERMISO = permiso.I_ID_PERMISO,
                        B_ESTADO = "1",
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                        D_FECHA_CREACION = _repositoryDate.Now,
                    }
                });

                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);
            }

            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                    new DetailError("06", "Registro ya existente (menú y rol) o registro inactivo")
                }
            };             
        }
    }
}
