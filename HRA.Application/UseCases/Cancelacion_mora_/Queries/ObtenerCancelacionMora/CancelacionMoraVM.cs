using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cancelacion_mora_.Queries.ObtenerCancelacionMora
{
    public record class CancelacionMoraVM : IRequest<Iresult>
    {
        public string? I_CANC_MORA_ID { get; set; }
    }
}
