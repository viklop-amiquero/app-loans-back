using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_Usuario;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_.Listado_usuarios
{
    public record class ListadoUsuariosDTO : IMapFrom<entity_Listado_usuario>
    {
        public int I_USER_ID { get; set; }
        public string? V_USER { get; set; }
        public string? V_FIRST_NAME { get; set; }
        public string? V_SURNAME { get; set; }
        public string? V_SECOND_SURNAME { get; set; }
        public DateTime? D_CREATE_DATE { get; set; }
        public string? B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_Listado_usuario, ListadoUsuariosDTO>()
                .ForMember(dto => dto.I_USER_ID, et => et.MapFrom(a => a.I_ID))
                .ForMember(dto => dto.V_USER, et => et.MapFrom(a => a.V_USUARIO))
                .ForMember(dto => dto.V_FIRST_NAME, et => et.MapFrom(a => a.V_PRIMER_NOMBRE))
                .ForMember(dto => dto.V_SURNAME, et => et.MapFrom(a => a.V_APELLIDO_PATERNO))
                .ForMember(dto => dto.V_SECOND_SURNAME, et => et.MapFrom(a => a.V_APELLIDO_MATERNO))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
