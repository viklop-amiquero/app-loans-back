using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Interes_credito_.Commands.ActivateInteresCredito
{
    public record class ActivateInteresCreditoVM : IRequest<Iresult>
    {
        public int I_INTEREST_ID { get; set; }
    }
}
