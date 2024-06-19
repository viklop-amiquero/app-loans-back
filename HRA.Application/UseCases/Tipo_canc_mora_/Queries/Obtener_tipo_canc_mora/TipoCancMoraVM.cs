using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Tipo_canc_mora_.Queries.Obtener_tipo_canc_mora
{
    public record class TipoCancMoraVM : IRequest<Iresult>
    {
        public string? I_TYPE_CANC_MORA_ID { get; set; }
    }
}
