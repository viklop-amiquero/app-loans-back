using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Cuota_.Commands.NewCuota
{
    public class NewCuotaHandler : IRequestHandler<NewCuotaVM, Iresult>
    {
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public NewCuotaHandler(
            IRepository<Cuota> cuotaRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor)
        {
            _repositoryCuota = cuotaRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(NewCuotaVM request, CancellationToken cancellationToken)
        {
            var identity = _httpContextAccesor as ClaimContextAccessor;
            //var usuario = _repositoryUsuario.Table.FirstOrDefault(x => x.V_NOMBRE == Convert.ToString(identity.UserName));

            //if (usuario == null)
            //{
            //    return new FailureResult<IEnumerable<DetailError>>()
            //    {
            //        StatusCode = 500,
            //        Value = new List<DetailError>()
            //        {
            //            new DetailError("05", "Usuario no autorizado")
            //        }
            //    };
            //}

            //request.V_NAME = request.V_NAME.ToUpper();
            //if (_repositoryCuota.TableNoTracking.Where(x => x.I_ID_CREDITO == int.Parse(request.V_ID_CREDIT)).ToList().Count == 0)
            //{

            //}

            _repositoryCuota.Insert(new List<Cuota>
                {
                    new Cuota
                    {
                        I_ID_CREDITO = int.Parse(request.V_ID_CREDIT),
                        I_MONTO_CUOTA = decimal.Parse(request.V_LOAN_AMOUNT),
                        I_CAPITAL = decimal.Parse(request.V_PRINCIPAL),
                        I_SALDO_INICIAL = decimal.Parse( request.V_BALANCE),
                        I_INTERES = decimal.Parse( request.V_INTEREST),
                        D_FECHA_PAGO = request.D_PAYMENT_DATE,
                        B_ESTADO = "1",
                        D_FECHA_CREACION = _repositoryDate.Now,
                    }
                });

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);

            //return new FailureResult<IEnumerable<DetailError>>()
            //{
            //    StatusCode = 400,
            //    Value = new List<DetailError>()
            //    {
            //        new DetailError("06", "Registro ya existente, el crédito para esta cuenta ya existe")
            //    }
            //};
        }

    }
}
