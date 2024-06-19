using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Tipo_mora_.Queries.ListaTipoMora
{
    public record class TiposMorasVM : IRequest<Iresult>
    {
    }
}
