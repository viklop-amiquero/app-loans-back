using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Interes_credito_.Commands.UpdateInteresCredito
{
    public class UpdateInteresCreditoHandler : IRequestHandler<UpdateInteresCreditoVM, Iresult>
    {
        private readonly IRepository<Interes_credito> _repositoryInteresCredito;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInteresCreditoHandler(
            IRepository<Interes_credito> interesCreditoRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor
            )
        {
            _repositoryInteresCredito = interesCreditoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }
        public async Task<Iresult> Handle(UpdateInteresCreditoVM request, CancellationToken cancellationToken)
        {
            int edad = 0;
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

            var entity = _repositoryInteresCredito.Table.FirstOrDefault(x => x.I_ID_INTERES_CREDITO == request.I_INTEREST_ID && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe la tasa de interes o está inactivo")
                    }
                };

            }
            request.V_NAME = request.V_NAME.ToUpper();

            if (_repositoryInteresCredito.TableNoTracking.Where(x => x.V_NOMBRE == request.V_NAME && x.I_ID_INTERES_CREDITO != request.I_INTEREST_ID ).ToList().Count == 0)
            {
                entity.V_NOMBRE = request.V_NAME == "" ? entity.V_NOMBRE : request.V_NAME;
                entity.I_TASA_INTERES = request.I_INTEREST == "" ? entity.I_TASA_INTERES : Convert.ToDecimal( request.I_INTEREST);
                entity.V_FRECUENCIA = request.V_FREQUENCY == "" ? entity.V_FRECUENCIA : request.V_FREQUENCY?.ToUpper();
                entity.V_DESCRIPCION = request.V_DESCRIPTION == "" ? entity.V_DESCRIPCION : request.V_DESCRIPTION;
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
