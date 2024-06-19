using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.EntitiesStoreProcedure;

namespace HRA.Application.UseCases.Login_.Queries.Authentication
{
    public record LoginDTO : IMapFrom<entity_Login>
    {
        public int IDUser { get; set; }
        public int IDPerson { get; set; }
        public string? User_name { get; set; }
        public string? Name { get; set; }
        public string? Paternal_surname { get; set; }
        public string? Maternal_surname { get; set; }
        public string? IDRole { get; set; }
        public string? Role { get; set; }
        public bool Change_password { get; set; }
        public string? State { get; set; }
        public void Mapping(Profile profine)
        {
            profine.CreateMap<entity_Login, LoginDTO>()
                .ForMember(dto => dto.IDUser, et => et.MapFrom(a => a.I_ID_USUARIO))
                .ForMember(dto => dto.IDPerson, et => et.MapFrom(a => a.I_ID_PERSONA))
                .ForMember(dto => dto.User_name, et => et.MapFrom(a => a.V_USUARIO))
                .ForMember(dto => dto.Name, et => et.MapFrom(a => a.V_PRIMER_NOMBRE))
                .ForMember(dto => dto.Paternal_surname, et => et.MapFrom(a => a.V_APELLIDO_PATERNO))
                .ForMember(dto => dto.Maternal_surname, et => et.MapFrom(a => a.V_APELLIDO_MATERNO))
                .ForMember(dto => dto.IDRole, et => et.MapFrom(a => a.I_ID_ROL))
                .ForMember(dto => dto.Role, et => et.MapFrom(a => a.V_ROL))
                .ForMember(dto => dto.Change_password, et => et.MapFrom(a => a.B_CAMBIO_CONTRASENIA))
                .ForMember(dto => dto.State, et => et.MapFrom(a => a.B_ESTADO));
        }

    }
}
