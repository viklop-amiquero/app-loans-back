using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Application;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Permiso_.Obtener_permiso
{
    public record class PermisoDTO : IMapFrom<Permiso>
    {
        public int I_PERMISSION_ID { get; set; }
        public int I_C { get; set; }
        public int I_R { get; set; }
        public int I_U { get; set; }
        public int I_D { get; set; }
        public string? V_DESCRIPTION { get; set; }
        public string B_STATE { get; set; } = string.Empty;
        public int? I_USER_CREATE { get; set; }
        public int? I_USER_MODIF { get; set; }
        public DateTime? D_CREATE_DATE { get; set; }
        public DateTime? D_MODIF_DATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Permiso, PermisoDTO>()
                .ForMember(dto => dto.I_PERMISSION_ID, et => et.MapFrom(a => a.I_ID_PERMISO))
                .ForMember(dto => dto.I_C, et => et.MapFrom(a => a.I_C))
                .ForMember(dto => dto.I_R, et => et.MapFrom(a => a.I_R))
                .ForMember(dto => dto.I_U, et => et.MapFrom(a => a.I_U))
                .ForMember(dto => dto.I_D, et => et.MapFrom(a => a.I_D))
                .ForMember(dto => dto.V_DESCRIPTION, et => et.MapFrom(a => a.V_DESCRIPCION))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA));
        }
    }
}
