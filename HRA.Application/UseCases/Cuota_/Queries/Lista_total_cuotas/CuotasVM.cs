using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cuota_.Queries.Lista_total_cuotas
{
    public record class CuotasVM : IRequest<Iresult>
    {
    }
}
