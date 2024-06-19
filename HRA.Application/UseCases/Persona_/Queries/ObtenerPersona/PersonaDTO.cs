using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Bussines;

namespace HRA.Application.UseCases.Persona_.Queries.ObtenerPersona
{
    public record class PersonaDTO : IMapFrom<Persona>
    {
        /// <summary>
        ///  parametros que se envian al front-end
        /// </summary>
        public int? I_PERSON_ID { get; set; }
        public int? I_UBIGEO_ID { get; set; }
        public int? I_SEX_ID { get; set; }
        public string? V_FIRST_NAME { get; set; }
        public string? V_SECOND_NAME { get; set; }
        public string? V_PATERNAL_LAST_NAME { get; set; }
        public string? V_MOTHER_LAST_NAME { get; set; }
        public int? I_AGE { get; set; }
        public DateTime? D_BIRTHDATE { get; set; }
        public string? V_ADDRESS_HOME { get; set; }
        public string? V_ADDRESS_WORK { get; set; }
        public string? B_STATE { get; set; }
        public string? V_DEPARTMENT { get; set; }
        public string? V_PROVINCE { get; set; }
        public string? V_DISTRICT { get; set; }

        public List<document_persona>? Document_persona { get; set; }
        public List<contacto_detail>? Contact { get; set; }

        public List<contacto_emergency>? Contact_emergency { get; set; }

        public int? I_POSITION_ID { get; set; }

        public class document_persona
        {
            public int? I_TYPE_DOCUMENT_ID { get; set; }
            public string? V_NRO_DOCUMENT { get; set; }
        }
        public class contacto_detail
        {
            public string? V_PHONE { get; set; }
            public string? V_MOVIL_PHONE { get; set; }
            public string? V_EMAIL { get; set; }
        }
        public class contacto_emergency
        {
            public string? V_NAME_RELATIONSHIP { get; set; }
            public string? V_RELATIONSHIP { get; set; }
            public string? V_MOVIL_PHONE_RELATIONSHIP { get; set; }
            public string? V_PHONE_RELATIONSHIP { get; set; }
        }
        public void Mapping(Profile profile)
        {
        }

    }
}
