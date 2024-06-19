using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Persona_.Queries.ObtenerPersonaXNDocument
{
    public record class PersonaXNDocumentVM : IRequest<Iresult>
    {
        /// <summary>
        ///  parametro que se recibe desde el front-end
        /// </summary>
        public string V_NRO_DOCUMENT { get; set; }
    }
}
