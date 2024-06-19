using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cancelacion_mora_.Queries.Lista_total_canc_mora
{
    public record class CancelacionesMoraVM : IRequest<Iresult>
    {
        public int I_MORA_ID { get; set; }
    }
}
