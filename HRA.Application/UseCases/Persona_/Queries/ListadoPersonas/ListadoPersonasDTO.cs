using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_Persona;

namespace HRA.Application.UseCases.Persona_.Queries.ListadoPersonas
{
    public record class ListadoPersonasDTO : IMapFrom<entity_Listado_persona>
    {
        public int I_PERSON_ID { get; set; }
        public string V_DNI { get; set; }
        public string V_PATERNAL_LAST_NAME { get; set; }
        public string V_MOTHER_LAST_NAME { get; set; }
        public string V_FIRST_NAME { get; set; }
        public string? V_ADDRESS { get; set; }
        public string? V_PROVINCE { get; set; }
        public string? V_DISTRICT { get; set; }
        public string V_MOVIL_PHONE { get; set; }
        public DateTime? D_DATE_CREATE { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profine)
        {
            profine.CreateMap<entity_Listado_persona, ListadoPersonasDTO>()
                .ForMember(dto => dto.I_PERSON_ID, et => et.MapFrom(a => a.I_ID))
                .ForMember(dto => dto.V_DNI, et => et.MapFrom(a => a.V_DNI))
                .ForMember(dto => dto.V_PATERNAL_LAST_NAME, et => et.MapFrom(a => a.V_APELLIDO_PATERNO))
                .ForMember(dto => dto.V_MOTHER_LAST_NAME, et => et.MapFrom(a => a.V_APELLIDO_MATERNO))
                .ForMember(dto => dto.V_FIRST_NAME, et => et.MapFrom(a => a.V_PRIMER_NOMBRE))
                .ForMember(dto => dto.V_ADDRESS, et => et.MapFrom(a => a.V_DIRECCION))
                .ForMember(dto => dto.V_PROVINCE, et => et.MapFrom(a => a.V_PROVINCIA))
                .ForMember(dto => dto.V_DISTRICT, et => et.MapFrom(a => a.V_DISTRITO))
                .ForMember(dto => dto.V_MOVIL_PHONE, et => et.MapFrom(a => a.V_CELULAR))
                .ForMember(dto => dto.D_DATE_CREATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
