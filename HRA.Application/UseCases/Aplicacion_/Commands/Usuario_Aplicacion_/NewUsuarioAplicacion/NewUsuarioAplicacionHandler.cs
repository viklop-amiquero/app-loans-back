using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_Aplicacion_.NewUsuarioAplicacion
{
    public class NewUsuarioAplicacionHandler : IRequestHandler<NewUsuarioAplicacionVM, Iresult>
    {
        private readonly IRepository<Usuario_Aplicacion> _repositoryUsuarioApp;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public NewUsuarioAplicacionHandler(
            IRepository<Usuario_Aplicacion> usuarioAppRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryUsuarioApp = usuarioAppRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(NewUsuarioAplicacionVM request, CancellationToken cancellationToken)
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

            var app_rol = _repositoryAppRolMenu.TableNoTracking.Where(x => x.I_ID_ROL.ToString() == request.I_ROLE_ID && x.B_ESTADO == "1").ToList();

            if (app_rol.Count() == 0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existen menús para este rol. Asigne menús para continuar")
                    }
                };
            }

            // Usuarios que tienen el rol ingresado
            var user_app = _repositoryUsuarioApp.TableNoTracking.Where(x => x.I_ID_APLICACION_ROL_MENU == app_rol[0].I_ID_APLICACION_ROL_MENU).ToList();

            // Filtrando los registros de app_rol_menu que no existen en usuario_aplicacion (nuevos)
            app_rol.ToList().ForEach(apm =>
            {
                if (_repositoryUsuarioApp.TableNoTracking.FirstOrDefault(x => x.I_ID_APLICACION_ROL_MENU == apm.I_ID_APLICACION_ROL_MENU) != null)
                {
                    app_rol.Remove(apm);
                }
            });

            // Se agregan los nuevos accesos a cada usuario que tenga ese rol asignado
            app_rol.ForEach(apm =>
            {
                user_app.ForEach(ua =>
                {
                    _repositoryUsuarioApp.Insert(new List<Usuario_Aplicacion>
                    {
                        new Usuario_Aplicacion
                        {
                            I_ID_USUARIO = ua.I_ID_USUARIO,
                            I_ID_APLICACION_ROL_MENU = apm.I_ID_APLICACION_ROL_MENU,
                            D_FECHA_INICIO = ua.D_FECHA_INICIO,
                            D_FECHA_FIN = ua.D_FECHA_FIN,
                            // Si un usuario esta inactivo o bloqueado, sus registros en Usuario_aplicacion estaran en 0, entonces al añadir mas 
                            // accesos a un rol, se debe tambien agregar estos registros de app_rol_menu al usuario con estado 0. En caso contrario, 
                            // si el usuario esta activo, agregar con estado 1. Esto se hace ya que si se desea activar al usuario luego de un tiempo, 
                            // se activaran todos sus registros de usuario_app incluyendo los ultimos accesos agregados a su rol.
                            B_ESTADO = ua.B_ESTADO,
                            I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                            D_FECHA_CREACION = _repositoryDate.Now,
                        }
                    });
                });
            });

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);

            //_repositoryAppRolMenu.TableNoTracking.Where(x => x.I_ID_ROL.ToString() == request.I_ROLE_ID && x.B_ESTADO == "1").ToList().ForEach(apm =>
            //{
            //    _repositoryUsuarioApp.Insert(new List<Usuario_Aplicacion>
            //    {
            //        new Usuario_Aplicacion
            //        {
            //            I_ID_USUARIO = Convert.ToInt32(request.I_USER_ID),
            //            I_ID_APLICACION_ROL_MENU = apm.I_ID_APLICACION_ROL_MENU,
            //            D_FECHA_INICIO = Convert.ToDateTime(request.D_START_DATE).Date,
            //            D_FECHA_FIN = request.D_END_DATE == "" ? null : request.D_END_DATE == "null" ? null : Convert.ToDateTime(request.D_END_DATE).Date,
            //            B_ESTADO = request.D_END_DATE == "" || request.D_END_DATE == "null" ? "1" : "0",
            //            I_USUARIO_CREACION = usuario.I_ID_USUARIO,
            //            D_FECHA_CREACION = _repositoryDate.Now,
            //        }
            //    });
            //});

            //if (_repositoryUsuarioApp.TableNoTracking.Where(
            //        x => x.I_ID_USUARIO.ToString() == request.I_USER_ID && x.I_ID_APLICACION_ROL_MENU.ToString() == request.I_APP_ROL_MENU_ID)
            //                    .ToList().Count == 0)
            //{
            //    _repositoryUsuarioApp.Insert(new List<Usuario_Aplicacion>
            //    {
            //        new Usuario_Aplicacion
            //        {
            //            I_ID_USUARIO = Convert.ToInt32(request.I_USER_ID),
            //            I_ID_APLICACION_ROL_MENU = Convert.ToInt32(request.I_APP_ROL_MENU_ID),
            //            D_FECHA_INICIO = Convert.ToDateTime(request.D_START_DATE).Date,
            //            D_FECHA_FIN = request.D_END_DATE == "" ? null : request.D_END_DATE == "null" ? null : Convert.ToDateTime(request.D_END_DATE).Date,
            //            B_ESTADO = request.D_END_DATE == "" || request.D_END_DATE == "null" ? "1" : "0",
            //            I_USUARIO_CREACION = usuario.I_ID_USUARIO,
            //            D_FECHA_CREACION = _repositoryDate.Now,
            //        }
            //    });

            //    await _unitOfWork.CommitChanges();
            //    return new SuccessResult<Unit>(Unit.Value);
            //}

            //return new FailureResult<IEnumerable<DetailError>>()
            //{
            //    StatusCode = 400,
            //    Value = new List<DetailError>()
            //    {
            //        new DetailError("06", "Registro ya existente (usuario y rol) o registro inactivo")
            //    }
            //};
        }
    }
}