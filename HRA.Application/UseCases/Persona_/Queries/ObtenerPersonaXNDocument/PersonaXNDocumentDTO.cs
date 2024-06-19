using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Bussines;

namespace HRA.Application.UseCases.Persona_.Queries.ObtenerPersonaXNDocument
{
    public record class PersonaXNDocumentDTO : IMapFrom<Persona>
    {
        /// <summary>
        ///  parametros que se envian al front-end
        /// </summary>
        public string V_NRO_DOCUMENT { get; set; }
        public int I_PERSON_ID { get; set; }
        public string V_FIRST_NAME { get; set; }
        public string? V_SECOND_NAME { get; set; }
        public string V_PATERNAL_LAST_NAME { get; set; }
        public string V_MOTHER_LAST_NAME { get; set; }
        
       
        public void Mapping(Profile profile)
        {
        }
    }
}
