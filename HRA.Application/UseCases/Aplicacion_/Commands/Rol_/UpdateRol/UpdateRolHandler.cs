using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Rol_.UpdateRol
{
    public class UpdateRolHandler : IRequestHandler<UpdateRolVM, Iresult>
    {
        private readonly IRepository<Rol> _repositoryRol;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRolHandler(
            IRepository<Rol> rolRepository, 
            IRepository<Usuario> usuarioRepository, 
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryRol = rolRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateRolVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryRol.Table.FirstOrDefault(x => x.I_ID_ROL == request.I_ROLE_ID && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el rol o está inactivo")
                    }
                };
            }

            request.V_ROLE = request.V_ROLE.ToUpper();
            if (_repositoryRol.TableNoTracking.Where(x => x.V_ROL.ToUpper() == request.V_ROLE
                                                                && x.I_ID_ROL != request.I_ROLE_ID).ToList().Count == 0)
            {
                entity.V_ROL = request.V_ROLE == "" ? entity.V_ROL : request.V_ROLE;
                entity.V_DESCRIPCION = request.V_DESCRIPTION == "" ? entity.V_DESCRIPCION : request.V_DESCRIPTION;
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
                    new DetailError("06", "Registro ya existente (nombre) o registro inactivo")
                }
            };
        }
    }
}
