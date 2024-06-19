using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_Aplicacion;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Application.Listado_Aplicaciones
{
    public record class ListadoAplicacionesDTO : IMapFrom<entity_Listado_aplicacion>
    {
        public int I_ID_APLICATION { get; set; }
        public string? V_APLICATION { get; set; }
        public string? V_ACRONYM { get; set; }
        public string? V_MENU { get; set; }
        public string? V_DESCRIPTION { get; set; }
        public string? V_URL { get; set; }
        public DateTime? D_DATE_CREATE { get; set; }
        public string? B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_Listado_aplicacion, ListadoAplicacionesDTO>()
                .ForMember(dto => dto.I_ID_APLICATION, et => et.MapFrom(a => a.I_ID))
                .ForMember(dto => dto.V_APLICATION, et => et.MapFrom(a => a.V_APLICACION))
                .ForMember(dto => dto.V_ACRONYM, et => et.MapFrom(a => a.V_ACRONIMO))
                .ForMember(dto => dto.V_MENU, et => et.MapFrom(a => a.V_MENU))
                .ForMember(dto => dto.V_DESCRIPTION, et => et.MapFrom(a => a.V_DESCRIPCION))
                .ForMember(dto => dto.V_URL, et => et.MapFrom(a => a.V_URL))
                .ForMember(dto => dto.D_DATE_CREATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
