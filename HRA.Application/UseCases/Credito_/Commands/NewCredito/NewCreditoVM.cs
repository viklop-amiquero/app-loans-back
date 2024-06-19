using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Credito_.Commands.NewCredito
{
    public record class NewCreditoVM : IRequest<Iresult>
    {
        public int I_PERSON_ID { get; set; }
        public int I_INTEREST_CREDIT_ID { get; set; }
        public string V_ID_TYPE_CREDIT { get; set; }
        public string V_ID_PAYMENT_FREQUENCY { get; set; }
        public string V_LOAN_AMOUNT { get; set; }
        public string V_TERM_QUANTITY { get; set; }
        public string D_DISBURSEMENT_DATE { get; set; }
    }
}
