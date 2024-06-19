using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Interes_credito_.Commands.DeleteInteresCredito
{
    public record class DeleteInteresCreditoVM : IRequest<Iresult>
    {
        /// <summary>
        /// delete interes credito
        /// </summary>
        public int I_INTEREST_ID { get; set; }
       
    }
}
