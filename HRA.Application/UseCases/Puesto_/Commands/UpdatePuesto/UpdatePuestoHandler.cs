using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Puesto_.Commands.UpdatePuesto
{
    public class UpdatePuestoHandler : IRequestHandler<UpdatePuestoVM, Iresult>
    {
        private readonly IRepository<Puesto> _repositoryPuesto;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePuestoHandler(
            IRepository<Puesto> puestoRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor
            )
        {
            _repositoryPuesto = puestoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }
        public async Task<Iresult> Handle(UpdatePuestoVM request, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccesor?.HttpContext?.User?.Claims;
            var claimUserId = claims?.FirstOrDefault(c => c.Type == "IDUser")?.Value;

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



            var entity = _repositoryPuesto.Table.FirstOrDefault(x => x.I_ID_PUESTO == request.I_POSITION_ID && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe la Puesto o está inactivo")
                    }
                };

            }
            request.V_NAME = request.V_NAME.ToString();
          
            if (_repositoryPuesto.TableNoTracking.Where(x => x.V_NOMBRE == request.V_NAME && x.I_ID_PUESTO != request.I_POSITION_ID).ToList().Count == 0)
            {
                   
                entity.V_NOMBRE = request.V_NAME == "" ? entity.V_NOMBRE : request.V_NAME;
                entity.V_DESCRIPCION = request.V_DESCRIPTION == "" ? entity.V_DESCRIPCION: request.V_DESCRIPTION;
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
                    new DetailError("06", "Registro ya existente")
                }
            };
        }
    }
}
