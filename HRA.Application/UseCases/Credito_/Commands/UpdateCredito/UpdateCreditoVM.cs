using HRA.Application.Common.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Credito_.Commands.UpdateCredito
{
    public record class UpdateCreditoVM : IRequest<Iresult>
    {
        public int I_CREDIT_ID { get; set; }
        public int I_ACCOUNT_ID { get; set; }
        public string V_INTEREST_CREDIT_ID { get; set; }
        public string V_ID_TYPE_CREDIT { get; set; }
        public string V_LOAN_AMOUNT { get; set; }
        public string V_PAYMENT_FREQUENCY { get; set; }
        public string V_TERM_QUANTITY { get; set; }
        public string V_DAY_PAY { get; set; }
        public DateTime D_DISBURSEMENT_DATE { get; set; }
        public DateTime D_DUE_DATE { get; set; }
    }
}
