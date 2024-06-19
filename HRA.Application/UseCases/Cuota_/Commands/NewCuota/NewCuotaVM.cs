using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cuota_.Commands.NewCuota
{
    public record class NewCuotaVM : IRequest<Iresult>
    {
        public string V_ID_CREDIT { get; set; }
        public string V_LOAN_AMOUNT { get; set; }
        public string V_PRINCIPAL { get; set; }
        public string V_BALANCE { get; set; }
        public string V_INTEREST { get; set; }
        public DateTime D_PAYMENT_DATE { get; set; }

    }
}
