using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cuota_.Commands.UpdateCuota
{
    public record class UpdateCuotaVM : IRequest<Iresult>
    {
        public int I_ID_INSTALLMENT { get; set; }
        public string V_ID_CREDIT { get; set; }
        public string V_LOAN_INSTALLMENT { get; set; }
        public string V_PRINCIPAL { get; set; }
        public string V_BALANCE { get; set; }
        public string V_INTEREST { get; set; }
        public DateTime D_PAYMENT_DATE { get; set; }
    }
}
