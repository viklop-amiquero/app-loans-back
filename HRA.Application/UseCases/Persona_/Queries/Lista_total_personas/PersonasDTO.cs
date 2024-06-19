using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Bussines;

namespace HRA.Application.UseCases.Persona_.Queries.Lista_total_personas
{
    public record class PersonasDTO : IMapFrom<Persona>
    {
        public int I_PERSON_ID { get; set; }
        public string? V_NRO_DOCUMENT { get; set; }
        public string? V_FIRST_NAME { get; set; }
        public string? V_PATERNAL_LAST_NAME { get; set; }
        public string? V_MOTHER_LAST_NAME { get; set; }
        public string? B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Persona, PersonasDTO>()
                .ForMember(dto => dto.I_PERSON_ID, et => et.MapFrom(a => a.I_ID_PERSONA))
                .ForMember(dto => dto.V_FIRST_NAME, et => et.MapFrom(a => a.V_PRIMER_NOMBRE))
                .ForMember(dto => dto.V_PATERNAL_LAST_NAME, et => et.MapFrom(a => a.V_APELLIDO_PATERNO))
                .ForMember(dto => dto.V_MOTHER_LAST_NAME, et => et.MapFrom(a => a.V_APELLIDO_MATERNO))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
