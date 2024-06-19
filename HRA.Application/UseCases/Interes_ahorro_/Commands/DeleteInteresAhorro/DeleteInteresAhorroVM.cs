using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Interes_ahorro_.Commands.DeleteInteresAhorro
{
    public record class DeleteInteresAhorroVM : IRequest<Iresult>
    {
        /// <summary>
        /// delete interes ahorro
        /// </summary>
        public int I_INTEREST_ID { get; set; }
       
    }
}
