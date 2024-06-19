using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Menu_.UpdateMenu
{
    public class UpdateMenuHandler : IRequestHandler<UpdateMenuVM, Iresult>
    {
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMenuHandler(
            IRepository<Menú> menuRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryMenu = menuRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateMenuVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryMenu.Table.FirstOrDefault(x => x.I_ID_MENU == request.I_MENU_ID && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el menú o está inactivo")
                    }
                };
            }

            var orden_cambio = 0;
            var menu_cambio = new Menú();
            if (request.I_ORDEN != "")
            {
                // Cambio de orden
                var menus = _repositoryMenu.Table.Where(x => x.I_ORDEN.ToString() == request.I_ORDEN && x.B_ESTADO == "1").ToList();

                if (menus.Count() == 0)
                {
                    return new FailureResult<IEnumerable<DetailError>>()
                    {
                        Value = new List<DetailError>()
                        {
                            new DetailError("02", "No existe un menú para el orden ingresado o está inactivo")
                        }
                    };
                }

                menu_cambio = entity.I_NIVEL == 1 ? menus.FirstOrDefault(x => x.I_NIVEL == 1 && x.I_ID_MENU != entity.I_ID_MENU)
                                    : menus.FirstOrDefault(x => x.I_NIVEL == 2 && x.V_PARENTESCO == entity.V_PARENTESCO && x.I_ID_MENU != entity.I_ID_MENU);

                if (menu_cambio == null)
                {
                    return new FailureResult<IEnumerable<DetailError>>()
                    {
                        Value = new List<DetailError>()
                        {
                            new DetailError("02", "No existe un menú para el orden ingresado o está inactivo o el orden es el mismo del menu a actualizar")
                        }
                    };
                }

                orden_cambio = menu_cambio.I_ORDEN;
            }

            request.V_NAME = request.V_NAME.ToUpper();
            if (_repositoryMenu.TableNoTracking.Where(x => (x.V_MENU == request.V_NAME && x.I_ID_APLICACION == entity.I_ID_APLICACION) 
                                                                && x.I_ID_MENU != request.I_MENU_ID).ToList().Count == 0)
            {
                entity.V_MENU = request.V_NAME == "" ? entity.V_MENU : request.V_NAME;
                entity.V_DESCRIPCION = request.V_DESCRIPTION == "" ? entity.V_DESCRIPCION : request.V_DESCRIPTION == "null" ? null : request.V_DESCRIPTION;
                entity.V_ICONO = request.V_ICON == "" ? entity.V_ICONO : request.V_ICON == "null" ? null : request.V_ICON;
                entity.V_RUTA = request.V_ROUTE == "" ? entity.V_RUTA : request.V_ROUTE == "null" ? null : request.V_ROUTE;
                entity.V_URL = request.V_URL == "" ? entity.V_URL : request.V_URL == "null" ? null : request.V_URL;
                // Cambio de orden
                if(request.I_ORDEN != "") menu_cambio.I_ORDEN = entity.I_ORDEN;
                entity.I_ORDEN = request.I_ORDEN == "" ? entity.I_ORDEN : orden_cambio;
                entity.B_ESTADO = "1";
                entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                entity.D_FECHA_MODIFICA = _repositoryDate.Now;

                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);
            }

            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                    new DetailError("06", "Registro ya existente (nombre y aplicación)")
                }
            };            
        }
    }
}
