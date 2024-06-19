using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Persona_.Queries.ObtenerPersona
{
    public record class PersonaVM : IRequest<Iresult>
    {
        /// <summary>
        ///  parametro que se recibe desde el front-end
        /// </summary>
        public int I_PERSON_ID { get; set; }
    }
}
