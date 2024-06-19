using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Persona_.Commands.ActivatePersona
{
    public record class ActivatePersonaVM : IRequest<Iresult>
    {
        public int I_PERSON_ID { get; set; }
    }
}
