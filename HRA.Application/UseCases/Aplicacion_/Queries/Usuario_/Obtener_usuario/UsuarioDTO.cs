using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Security;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_.Obtener_usuario
{
    public record class UsuarioDTO : IMapFrom<Usuario>
    {
        public int I_USER_ID { get; set; }
        public int I_PERSON_ID { get; set; }
        public string V_USER { get; set; } = string.Empty;
        public string B_STATE { get; set; } = string.Empty;
        public int? I_USER_CREATE { get; set; }
        public int? I_USER_MODIF { get; set; }
        public DateTime? D_CREATE_DATE { get; set; }
        public DateTime? D_MODIF_DATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dto => dto.I_USER_ID, et => et.MapFrom(a => a.I_ID_USUARIO))
                .ForMember(dto => dto.I_PERSON_ID, et => et.MapFrom(a => a.I_ID_PERSONA))
                .ForMember(dto => dto.V_USER, et => et.MapFrom(a => a.V_USUARIO))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom (a => a.B_ESTADO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA));
        }
    }
}