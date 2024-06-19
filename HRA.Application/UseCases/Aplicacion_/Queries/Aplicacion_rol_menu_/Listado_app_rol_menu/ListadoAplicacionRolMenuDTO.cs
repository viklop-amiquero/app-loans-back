using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_AplicacionRolMenu;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_rol_menu_.Listado_app_rol_menu
{
    public record class ListadoAplicacionRolMenuDTO : IMapFrom<entity_Listado_aplicacion_rol_menu>
    {
        public int I_ID_APP_ROLE_MENU { get; set; }
        public string? V_ROLE { get; set; }
        //public string? V_APP { get; set; }
        public string? V_MENU { get; set; }
        public string? V_PERMISSION { get; set; }
        public DateTime? D_DATE_CREATE { get; set; }
        public string? B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_Listado_aplicacion_rol_menu, ListadoAplicacionRolMenuDTO>()
                .ForMember(dto => dto.I_ID_APP_ROLE_MENU, et => et.MapFrom(a => a.I_ID))
                .ForMember(dto => dto.V_ROLE, et => et.MapFrom(a => a.V_ROL))
                //.ForMember(dto => dto.V_APP, et => et.MapFrom(a => a.V_APP))
                .ForMember(dto => dto.V_MENU, et => et.MapFrom(a => a.V_MENU))
                .ForMember(dto => dto.V_PERMISSION, et => et.MapFrom(a => a.V_PERMISO))
                .ForMember(dto => dto.D_DATE_CREATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
