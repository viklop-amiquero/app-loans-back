using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Mora_.Queries.Lista_moras_credito
{
    public record class MorasCreditoVM : IRequest<Iresult>
    {
        public int I_CREDIT_ID { get; set; }
    }
}
