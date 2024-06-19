using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Menu_access_
{
    public class UpdateMenuAccessHandler : IRequestHandler<UpdateMenuAccessVM, Iresult>
    {
        private readonly IRepository<Usuario_Aplicacion> _repositoryUsuarioApp;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMenuAccessHandler(
            IRepository<Usuario_Aplicacion> usuarioAppRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Menú> menuRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryUsuarioApp = usuarioAppRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryMenu = menuRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateMenuAccessVM request, CancellationToken cancellationToken)
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

            var menu = _repositoryMenu.TableNoTracking.FirstOrDefault(x => x.I_ID_MENU == request.I_MENU_ID && x.B_ESTADO == "1");

            if (menu is null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "El menú no existe o está inactivo.")
                    }
                };
            }

            var app_roles = new List<int>();
            var app_rol_menu = new List<int>();
            bool res = false;

            if (menu.I_NIVEL == 2)
            {
                app_rol_menu = _repositoryAppRolMenu.TableNoTracking.Join(_repositoryMenu.TableNoTracking.ToList(), ARM => ARM.I_ID_MENU,
                                    M => M.I_ID_MENU, (ARM, M) => new { ARM, M }).Where(w => w.ARM.B_ESTADO == "1" && w.M.I_NIVEL == 1
                                    && w.M.V_NIVEL_PARENTESCO.ToString() == menu.V_PARENTESCO).Select(x => x.ARM.I_ID_APLICACION_ROL_MENU).ToList();
            }

            if (app_rol_menu.Count() != 0)
            {
                res = true;
                app_rol_menu.ForEach(x =>
                {
                    _repositoryAppRolMenu.Table.Where(w => w.I_ID_APLICACION_ROL_MENU == x).ToList().ForEach(a =>
                    {
                        a.I_ID_MENU = menu.I_ID_MENU;
                        a.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                        a.D_FECHA_MODIFICA = _repositoryDate.Now;
                    });
                });
            }

            if (!res)
            {
                app_roles = _repositoryAppRolMenu.TableNoTracking.Select(x => x.I_ID_ROL).Distinct().ToList();
                
                app_roles.ForEach(r =>
                {
                    var rol = _repositoryAppRolMenu.TableNoTracking.FirstOrDefault(x => x.I_ID_ROL == r);
                    var access_menu = new Aplicacion_Rol_Menu
                    {
                        I_ID_MENU = menu.I_ID_MENU,
                        I_ID_ROL = r,
                        I_ID_PERMISO = 5,
                        B_ESTADO = rol.B_ESTADO,
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                        D_FECHA_CREACION = _repositoryDate.Now
                    };
                    _repositoryAppRolMenu.Insert(new List<Aplicacion_Rol_Menu> { access_menu });

                    var users = _repositoryUsuarioApp.TableNoTracking.Where(x => x.I_ID_APLICACION_ROL_MENU == rol.I_ID_APLICACION_ROL_MENU).ToList();
                    users.ForEach(u =>
                    {
                        _repositoryUsuarioApp.Insert(new List<Usuario_Aplicacion>
                        {
                            new Usuario_Aplicacion
                            {
                                I_ID_USUARIO = u.I_ID_USUARIO,
                                I_ID_APLICACION_ROL_MENU = access_menu.I_ID_APLICACION_ROL_MENU,
                                D_FECHA_INICIO = u.D_FECHA_INICIO,
                                D_FECHA_FIN = u.D_FECHA_FIN,
                                // Si un usuario esta inactivo o bloqueado, sus registros en Usuario_aplicacion estaran en 0, entonces al añadir mas 
                                // accesos a un rol, se debe tambien agregar estos registros de app_rol_menu al usuario con estado 0. En caso contrario, 
                                // si el usuario esta activo, agregar con estado 1. Esto se hace ya que si se desea activar al usuario luego de un tiempo, 
                                // se activaran todos sus registros de usuario_app incluyendo los ultimos accesos agregados a su rol.
                                B_ESTADO = u.B_ESTADO,
                                I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                                D_FECHA_CREACION = _repositoryDate.Now
                            }
                        });
                    });
                });   
            }

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
