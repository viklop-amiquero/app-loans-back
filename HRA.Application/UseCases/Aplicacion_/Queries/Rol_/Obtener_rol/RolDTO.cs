using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Application;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Rol_.Obtener_rol
{
    public record class RolDTO : IMapFrom<Rol>
    {
        public int I_ROLE_ID { get; set; }
        public string V_ROLE { get; set; } = string.Empty;
        public string V_DESCRIPTION { get; set; } = string.Empty;
        public string B_STATE { get; set; } = string.Empty;
        public int? I_USER_CREATE { get; set; }
        public int? I_USER_MODIF { get; set; }
        public DateTime? D_CREATE_DATE { get; set; }
        public DateTime? D_MODIF_DATE { get; set; }
    
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Rol, RolDTO>()
                .ForMember(dto => dto.I_ROLE_ID, et => et.MapFrom(a => a.I_ID_ROL))
                .ForMember(dto => dto.V_ROLE, et => et.MapFrom(a => a.V_ROL))
                .ForMember(dto => dto.V_DESCRIPTION, et => et.MapFrom(a => a.V_DESCRIPCION))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA));
        }
    }
}
