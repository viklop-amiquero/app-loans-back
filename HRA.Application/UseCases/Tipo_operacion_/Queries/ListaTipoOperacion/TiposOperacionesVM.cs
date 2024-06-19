using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Tipo_operacion_.Queries.ListaTipoOperacion
{
    public record class TiposOperacionesVM : IRequest<Iresult>
    {
    }
}
