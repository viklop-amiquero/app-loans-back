using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Application;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_rol_menu_.Obtener_app_rol_menu
{
    public record class AppRolMenuDTO : IMapFrom<Aplicacion_Rol_Menu>
    {
        public int I_APP_ROL_MENU_ID { get; set; }
        public int I_APP_ID { get; set; }
        public int I_MENU_ID { get; set; }
        public int I_ROL_ID { get; set; }
        public int I_PERMISSION_ID { get; set; }
        public string B_STATE { get; set; } = string.Empty;
        public int I_USER_CREATE { get; set; }
        public int I_USER_MODIF { get; set; }
        public DateTime D_CREATE_DATE { get; set; }
        public DateTime D_MODIF_DATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Aplicacion_Rol_Menu, AppRolMenuDTO>()
                .ForMember(dto => dto.I_APP_ROL_MENU_ID, et => et.MapFrom(a => a.I_ID_APLICACION_ROL_MENU))
                .ForMember(dto => dto.I_MENU_ID, et => et.MapFrom(a => a.I_ID_MENU))
                .ForMember(dto => dto.I_ROL_ID, et => et.MapFrom(a => a.I_ID_ROL))
                .ForMember(dto => dto.I_PERMISSION_ID, et => et.MapFrom(a => a.I_ID_PERMISO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
