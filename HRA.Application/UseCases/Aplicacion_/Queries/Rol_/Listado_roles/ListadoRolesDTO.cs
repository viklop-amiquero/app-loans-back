using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure.SP_Rol;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Rol_.Listado_roles
{
    public record class ListadoRolesDTO : IMapFrom<entity_Listado_rol>
    {
        public int I_ROLE_ID { get; set; }
        public string? V_ROLE { get; set; }
        public string? V_DESCRIPTION { get; set; }
        public DateTime? D_CREATE_DATE { get; set; }
        public string? B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<entity_Listado_rol, ListadoRolesDTO>()
                .ForMember(dto => dto.I_ROLE_ID, et => et.MapFrom(a => a.I_ID))
                .ForMember(dto => dto.V_ROLE, et => et.MapFrom(a => a.V_ROL))
                .ForMember(dto => dto.V_DESCRIPTION, et => et.MapFrom(a => a.V_DESCRIPCION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
