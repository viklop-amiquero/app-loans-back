using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Operacion_.Queries.Lista_total_operaciones
{
    public record class OperacionesVM : IRequest<Iresult>
    {
    }
}
