using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Interes_ahorro_.Commands.ActivateInteresAhorro
{
    public record class ActivateInteresAhorroVM : IRequest<Iresult>
    {
        public int I_INTEREST_ID { get; set; }
    }
}
