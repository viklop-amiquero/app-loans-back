using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Persona_.Commands.DeletePersona
{
    public record class DeletePersonaVM : IRequest<Iresult>
    {
        /// <summary>
        /// delete persona
        /// </summary>
        public int I_PERSON_ID { get; set; }
       
    }
}
