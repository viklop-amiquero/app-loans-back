using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Persona_.Queries.Lista_total_personas
{
    public record class PersonasVM : IRequest<Iresult>
    {
    }
}
