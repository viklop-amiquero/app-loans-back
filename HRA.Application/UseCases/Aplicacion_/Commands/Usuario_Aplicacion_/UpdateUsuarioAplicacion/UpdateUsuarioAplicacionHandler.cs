using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_Aplicacion_.UpdateUsuarioAplicacion
{
    public class UpdateUsuarioAplicacionHandler : IRequestHandler<UpdateUsuarioAplicacionVM, Iresult>
    {
        private readonly IRepository<Usuario_Aplicacion> _repositoryUsuarioApp;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUsuarioAplicacionHandler(
            IRepository<Usuario_Aplicacion> usuarioAppRepository,
            IRepository<Usuario> usuarioRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryUsuarioApp = usuarioAppRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateUsuarioAplicacionVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryUsuarioApp.Table.Where(x => x.I_ID_USUARIO == request.I_USER_ID && x.B_ESTADO == "1").ToList();

            if (entity.Count() == 0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existen registros para este usuario o están inactivos")
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

            for (int i = 0; i < entity.Count(); i++)
            {
                entity[i].I_ID_APLICACION_ROL_MENU = request.I_ROLE_ID == "" ? entity[i].I_ID_APLICACION_ROL_MENU : app_rol[i].I_ID_APLICACION_ROL_MENU;
                entity[i].D_FECHA_INICIO = request.D_START_DATE == "" ? entity[i].D_FECHA_INICIO : Convert.ToDateTime(request.D_START_DATE).Date;
                entity[i].D_FECHA_FIN = request.D_END_DATE == "" ? entity[i].D_FECHA_FIN : Convert.ToDateTime(request.D_END_DATE).Date;
                //entity[i].B_ESTADO = request.D_END_DATE == "" ? entity[i].B_ESTADO : "0";
                entity[i].B_ESTADO = entity[i].B_ESTADO;
                entity[i].I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                entity[i].D_FECHA_MODIFICA = _repositoryDate.Now;
            }
                        
            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
