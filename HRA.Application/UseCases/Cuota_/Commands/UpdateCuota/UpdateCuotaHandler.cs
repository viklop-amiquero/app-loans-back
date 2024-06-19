using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Cuota_.Commands.UpdateCuota
{
    public class UpdateCuotaHandler : IRequestHandler<UpdateCuotaVM, Iresult>
    {
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCuotaHandler(
            IRepository<Cuota> cuotaRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryCuota = cuotaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateCuotaVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryCuota.Table.FirstOrDefault(x => x.I_ID_CUOTA == request.I_ID_INSTALLMENT && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe el crédito o está inactivo")
                    }
                };

            }

            //request.V_NAME = request.V_NAME.ToUpper();
            if (_repositoryCuota.TableNoTracking.Where(x => x.I_ID_CUOTA == request.I_ID_INSTALLMENT).ToList().Count != 0)
            {
                entity.I_MONTO_CUOTA = request.V_LOAN_INSTALLMENT == "" ? entity.I_MONTO_CUOTA : decimal.Parse( request.V_LOAN_INSTALLMENT);
                entity.I_CAPITAL = request.V_PRINCIPAL == "" ? entity.I_CAPITAL : decimal.Parse(request.V_PRINCIPAL);
                entity.I_SALDO_INICIAL = request.V_BALANCE == "" ? entity.I_SALDO_INICIAL : decimal.Parse(request.V_BALANCE);
                entity.I_INTERES = request.V_INTEREST == "" ? entity.I_INTERES : decimal.Parse(request.V_INTEREST);
                entity.D_FECHA_PAGO = request.D_PAYMENT_DATE == null ? entity.D_FECHA_PAGO : request.D_PAYMENT_DATE;
                entity.B_ESTADO = "1";
                //entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                entity.D_FECHA_MODIFICA = _repositoryDate.Now;

                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);
            }

            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                    new DetailError("06", "Credito o cuenta inexistente")
                }
            };
        }
    }
}
