using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Tipo_canc_mora_.Queries.Lista_total_tipos_canc
{
    public record class TiposCancMoraVM : IRequest<Iresult>
    {
    }
}
