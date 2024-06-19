using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Credito_.Queries.Lista_total_creditos
{
    public record class CreditosVM : IRequest<Iresult>
    {
    }
}
