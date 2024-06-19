using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Menu_.NewMenu
{
    public class NewMenuHandler : IRequestHandler<NewMenuVM, Iresult>
    {
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IRepository<Aplicacion> _repositoryApp;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public NewMenuHandler(
            IRepository<Menú> menuRepository,
            IRepository<Aplicacion> appRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryMenu = menuRepository;
            _repositoryApp = appRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(NewMenuVM request, CancellationToken cancellationToken)
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

            var app = _repositoryApp.TableNoTracking.FirstOrDefault(x => x.I_ID_APLICACION.ToString() == request.I_APPLICATION_ID && x.B_ESTADO == "1");

            if (app == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe la aplicación o está inactiva")
                    }
                };
            }

            var menus_padres = _repositoryMenu.TableNoTracking.Where(x => x.I_NIVEL == 1 && x.B_ESTADO == "1").ToList();

            var menu_padre = _repositoryMenu.TableNoTracking.FirstOrDefault(x => x.I_NIVEL == 1 && x.I_ID_MENU.ToString() == request.V_ID_MENU_PADRE && x.B_ESTADO == "1");

            if (request.V_ID_MENU_PADRE != "" && menu_padre == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el menú padre o está inactivo")
                    }
                };
            }

            var submenus = menu_padre == null ? null : _repositoryMenu.TableNoTracking.Where(x => x.I_NIVEL == 2 && x.B_ESTADO == "1" 
                                                            && x.V_PARENTESCO == menu_padre.V_NIVEL_PARENTESCO.ToString().ToUpper()).ToList();

            request.V_NAME = request.V_NAME.ToUpper();
            if (_repositoryMenu.TableNoTracking.Where(x => x.V_MENU == request.V_NAME 
                                            && x.I_ID_APLICACION == app.I_ID_APLICACION).ToList().Count == 0)
            {
                _repositoryMenu.Insert(new List<Menú>
                {
                    new Menú
                    {
                        I_ID_APLICACION = app.I_ID_APLICACION,
                        V_MENU = request.V_NAME,
                        V_DESCRIPCION = request.V_DESCRIPTION == "" ? null : request.V_DESCRIPTION == "null" ? null : request.V_DESCRIPTION,
                        V_ICONO = request.V_ICON ==  "" ? null : request.V_ICON == "null" ? null : request.V_ICON,
                        V_RUTA = request.V_ROUTE ==  "" ? null : request.V_ROUTE == "null" ? null : request.V_ROUTE,
                        V_URL = request.V_URL ==  "" ? null : request.V_URL == "null" ? null : request.V_URL,
                        V_NIVEL_PARENTESCO = Guid.NewGuid(),
                        V_PARENTESCO = menu_padre == null ? null : menu_padre.V_NIVEL_PARENTESCO.ToString().ToUpper(),
                        I_NIVEL = menu_padre == null ? 1 : 2,
                        //I_ORDEN = menu_padre == null ? menus.MaxBy(x => x.I_ORDEN)!.I_ORDEN + 1 : submenus!.MaxBy(x => x.I_ORDEN)!.I_ORDEN + 1,
                        I_ORDEN = menu_padre == null ? (menus_padres.Count() == 0 ? 1 : menus_padres.MaxBy(x => x.I_ORDEN)!.I_ORDEN + 1) 
                                                        : (submenus!.Count() == 0 ? 1 : submenus!.MaxBy(x => x.I_ORDEN)!.I_ORDEN + 1),
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
                     new DetailError("06", "Registro ya existente (menu y aplicación) o registro inactivo")
                    }
            };
        }
    }
}
