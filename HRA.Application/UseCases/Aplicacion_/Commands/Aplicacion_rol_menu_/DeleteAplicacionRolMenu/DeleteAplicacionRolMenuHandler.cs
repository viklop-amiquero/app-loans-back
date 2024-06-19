using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.DeleteAplicacionRolMenu
{
    public class DeleteAplicacionRolMenuHandler : IRequestHandler<DeleteAplicacionRolMenuVM, Iresult>
    {
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Usuario_Aplicacion> _repositoryUsuarioApp;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAplicacionRolMenuHandler(
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Usuario_Aplicacion> usuarioAppRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryUsuarioApp = usuarioAppRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(DeleteAplicacionRolMenuVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryAppRolMenu.Table.FirstOrDefault(x => x.I_ID_APLICACION_ROL_MENU == request.I_APPLICATION_ROLE_MENU_ID && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el registro o ya está inactivo")
                    }
                };
            }

            entity.B_ESTADO = "0";
            entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            _repositoryUsuarioApp.Table.Where(x => x.I_ID_APLICACION_ROL_MENU == entity.I_ID_APLICACION_ROL_MENU).ToList().ForEach(ua =>
            {
                ua.B_ESTADO = "0";
                ua.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                ua.D_FECHA_MODIFICA = _repositoryDate.Now;
            });

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
