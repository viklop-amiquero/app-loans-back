using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Application;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Application.Obtener_aplicacion
{
    public record class AplicacionDTO : IMapFrom<Aplicacion>
    {
        public int I_APPLICATION_ID { get; set; }
        public string V_APPLICATION { get; set; } = string.Empty;
        public string? V_ACRONYM { get; set; }
        public string? V_DESCRIPTION { get; set; }
        public string? V_URL { get; set; }
        public string B_STATE { get; set; } = string.Empty;
        public int I_USER_CREATE { get; set; }
        public int I_USER_MODIF { get; set; }
        public DateTime D_CREATE_DATE { get; set; }
        public DateTime D_MODIF_DATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Aplicacion, AplicacionDTO>()
                .ForMember(dto => dto.I_APPLICATION_ID, et => et.MapFrom(a => a.I_ID_APLICACION))
                .ForMember(dto => dto.V_APPLICATION, et => et.MapFrom(a => a.V_APLICACION))
                .ForMember(dto => dto.V_ACRONYM, et => et.MapFrom(a => a.V_ACRONIMO))
                .ForMember(dto => dto.V_DESCRIPTION, et => et.MapFrom(a => a.V_DESCRIPCION))
                .ForMember(dto => dto.V_URL, et => et.MapFrom(a => a.V_URL))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
