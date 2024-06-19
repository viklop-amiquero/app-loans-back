using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Application_.DeleteAplicacion
{
    public class DeleteAplicacionHandler : IRequestHandler<DeleteAplicacionVM, Iresult>
    {
        private readonly IRepository<Aplicacion> _repositoryAplicacion;
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Usuario_Aplicacion> _repositoryUserAplicacion;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAplicacionHandler(
            IRepository<Aplicacion> aplicacionRepository,
            IRepository<Menú> menuRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Usuario_Aplicacion> aplicacionUserRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryAplicacion = aplicacionRepository;
            _repositoryMenu = menuRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryUserAplicacion = aplicacionUserRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(DeleteAplicacionVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryAplicacion.Table.FirstOrDefault(x => x.I_ID_APLICACION == request.I_APLICATION_ID && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe la aplicación o ya está inactiva")
                    }
                };
            }       

            entity.B_ESTADO = "0";
            entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            _repositoryMenu.Table.Where(x => x.I_ID_APLICACION == entity.I_ID_APLICACION).ToList().ForEach(m =>
            {
                m.B_ESTADO = "0";
                m.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                m.D_FECHA_MODIFICA = _repositoryDate.Now;

                _repositoryAppRolMenu.Table.Where(x => x.I_ID_MENU == m.I_ID_MENU).ToList().ForEach(apm =>
                {
                    apm.B_ESTADO = "0";
                    apm.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                    apm.D_FECHA_MODIFICA = _repositoryDate.Now;

                    _repositoryUserAplicacion.Table.Where(x => x.I_ID_APLICACION_ROL_MENU == apm.I_ID_APLICACION_ROL_MENU).ToList().ForEach(ua =>
                    {
                        ua.B_ESTADO = "0";
                        ua.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                        ua.D_FECHA_MODIFICA = _repositoryDate.Now;
                    });
                });
            });

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
