using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.UpdateAplicacionRolMenu
{
    public class UpdateAplicacionRolMenuHandler : IRequestHandler<UpdateAplicacionRolMenuVM, Iresult>
    {
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Permiso> _repositoryPermiso;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAplicacionRolMenuHandler(
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Permiso> permisoRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryPermiso = permisoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime; 
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateAplicacionRolMenuVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryAppRolMenu.Table.FirstOrDefault(
                    x => x.I_ID_APLICACION_ROL_MENU == request.I_APPLICATION_ROLE_MENU_ID && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el acceso o está inactivo")
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

            entity.I_ID_PERMISO = request.I_PERMISSION_ID == "" ? entity.I_ID_PERMISO : permiso.I_ID_PERMISO;
            entity.B_ESTADO = "1";
            entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
