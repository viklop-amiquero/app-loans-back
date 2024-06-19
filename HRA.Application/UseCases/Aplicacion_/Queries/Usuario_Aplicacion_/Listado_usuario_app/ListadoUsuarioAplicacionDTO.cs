using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_Usuario_Aplicacion;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Listado_usuario_app
{
    public record class ListadoUsuarioAplicacionDTO : IMapFrom<entity_Listado_usuario_app>
    {
        public int I_ID_USER { get; set; }
        public string? V_USER { get; set; }
        public string? V_FULL_NAME { get; set; }
        public string? V_ROLE { get; set; }
        public DateTime? D_START_DATE { get; set; }
        public DateTime? D_END_DATE { get; set; }
        public string? B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_Listado_usuario_app, ListadoUsuarioAplicacionDTO>()
                .ForMember(dto => dto.I_ID_USER, et => et.MapFrom(a => a.I_ID))
                .ForMember(dto => dto.V_USER, et => et.MapFrom(a => a.V_USUARIO))
                .ForMember(dto => dto.V_FULL_NAME, et => et.MapFrom(a => a.V_NOMBRE_COMPLETO))
                .ForMember(dto => dto.V_ROLE, et => et.MapFrom(a => a.V_ROL))
                .ForMember(dto => dto.D_START_DATE, et => et.MapFrom(a => a.D_FECHA_INICIO))
                .ForMember(dto => dto.D_END_DATE, et => et.MapFrom(a => a.D_FECHA_FIN))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
