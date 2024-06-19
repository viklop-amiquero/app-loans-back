using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Credito_.Commands.DeleteCredito
{
    public record class DeleteCreditoVM : IRequest<Iresult>
    {
        public int I_CREDITO_ID { get; set; }
    }
}
