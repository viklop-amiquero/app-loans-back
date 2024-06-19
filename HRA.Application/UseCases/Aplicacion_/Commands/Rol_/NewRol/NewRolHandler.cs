using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Rol_.NewRol
{
    public class NewRolHandler : IRequestHandler<NewRolVM, Iresult>
    {
        private readonly IRepository<Rol> _repositoryRol;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public NewRolHandler(
            IRepository<Rol> rolRepository,
            IRepository<Usuario> userRepository,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IUnitOfWork unitOfWork)
        {
            _repositoryRol = rolRepository;
            _repositoryUsuario = userRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(NewRolVM request, CancellationToken cancellationToken)
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

            request.V_ROLE = request.V_ROLE.ToUpper();

            if (_repositoryRol.TableNoTracking.Where(x => x.V_ROL.ToUpper() == request.V_ROLE).ToList().Count == 0)
            {
                _repositoryRol.Insert(new List<Rol>
                {
                    new Rol
                    {
                        V_ROL = request.V_ROLE,
                        V_DESCRIPCION = request.V_DESCRIPTION,
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
                    new DetailError("06", "Registro ya existente (nombre) o registro inactivo")
                }
            };
        }
    }
}
