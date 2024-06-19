using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Application;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Menu_.Obtener_menu
{
    public record class MenuDTO : IMapFrom<Menú>
    {
        public int I_MENU_ID { get; set; }
        public int I_APPLICATION_ID { get; set; }
        public string V_NAME { get; set; } = string.Empty;
        public string? V_DESCRIPTION { get; set; }
        public string? V_ICON { get; set; }
        public string? V_ROUTE { get; set; }
        public string? V_URL { get; set; }
        public string V_RELATIONSHIP_ID { get; set; } = string.Empty;
        public string? V_RELATIONSHIP { get; set; }
        //public int I_LEVEL { get; set; }
        //public int I_ORDER { get; set; }
        public string? B_STATE { get; set; } = string.Empty;
        public int I_USER_CREATE { get; set; }
        public int I_USER_MODIF { get; set; }
        public DateTime D_CREATE_DATE { get; set; }
        public DateTime D_MODIF_DATE { get; set; }      

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Menú, MenuDTO>()
                .ForMember(dto => dto.I_MENU_ID, et => et.MapFrom(a => a.I_ID_MENU))
                .ForMember(dto => dto.I_APPLICATION_ID, et => et.MapFrom(a => a.I_ID_APLICACION))
                .ForMember(dto => dto.V_NAME, et => et.MapFrom(a => a.V_MENU))
                .ForMember(dto => dto.V_DESCRIPTION, et => et.MapFrom(a => a.V_DESCRIPCION))
                .ForMember(dto => dto.V_ICON, et => et.MapFrom(a => a.V_ICONO))
                .ForMember(dto => dto.V_ROUTE, et => et.MapFrom(a => a.V_RUTA))
                .ForMember(dto => dto.V_URL, et => et.MapFrom(a => a.V_URL))
                .ForMember(dto => dto.V_RELATIONSHIP_ID, et => et.MapFrom(a => a.V_NIVEL_PARENTESCO))
                .ForMember(dto => dto.V_RELATIONSHIP, et => et.MapFrom(a => a.V_PARENTESCO))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA));
        }
    }
}
