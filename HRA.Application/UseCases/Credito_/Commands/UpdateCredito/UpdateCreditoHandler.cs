using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Credito_.Commands.UpdateCredito
{
    public class UpdateCreditoHandler : IRequestHandler<UpdateCreditoVM, Iresult>
    {
        private readonly IRepository<Credito> _repositoryCredito;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCreditoHandler(
            IRepository<Credito> creditoRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryCredito = creditoRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateCreditoVM request, CancellationToken cancellationToken)
        {
            var identity = _httpContextAccesor as ClaimContextAccessor;

            var entity = _repositoryCredito.Table.FirstOrDefault(x => x.I_ID_CREDITO == request.I_CREDIT_ID && x.B_ESTADO == "1");

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
            if (_repositoryCredito.TableNoTracking.Where(x => x.I_ID_CUENTA == request.I_ACCOUNT_ID).ToList().Count != 0)
            {
                //entity.I_ID_CUENTA = request.I_ID_ACCOUNT == null ? entity.I_ID_CUENTA : request.I_ID_ACCOUNT;
                entity.I_ID_TIPO_CREDITO = request.V_ID_TYPE_CREDIT == "" ? entity.I_ID_TIPO_CREDITO :int.Parse (request.V_ID_TYPE_CREDIT);
                entity.I_MONTO_PRESTAMO = request.V_LOAN_AMOUNT == "" ? entity.I_MONTO_PRESTAMO : decimal.Parse(request.V_LOAN_AMOUNT);
                //entity.V_FRECUENCIA_PAGO = request.V_PAYMENT_FREQUENCY == "" ? entity.V_FRECUENCIA_PAGO :request.V_PAYMENT_FREQUENCY;
                entity.I_PLAZO_CANTIDAD = request.V_TERM_QUANTITY == "" ? entity.I_PLAZO_CANTIDAD : int.Parse(request.V_TERM_QUANTITY);
                //entity.I_DIA_PAGO = request.V_DAY_PAY == "" ? entity.I_DIA_PAGO : int.Parse(request.V_DAY_PAY);
                entity.I_ID_INTERES_CREDITO = request.V_INTEREST_CREDIT_ID == "" ? entity.I_ID_INTERES_CREDITO : Convert.ToInt32(request.V_INTEREST_CREDIT_ID);
                entity.D_FECHA_DESEMBOLSO = request.D_DISBURSEMENT_DATE == null ? entity.D_FECHA_DESEMBOLSO : request.D_DISBURSEMENT_DATE;

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
