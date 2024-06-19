using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Operaciones;

namespace HRA.Application.UseCases.Puesto_.Queries.Obtener_puesto
{
    public record class PuestoDTO : IMapFrom<Puesto>
    {
        public int I_PUESTO_ID { get; set; }
        public string? V_NAME { get; set; }
        public string? B_STATE { get; set; }
        public int I_USER_CREATE { get; set; }
        public int I_USER_MODIF { get; set; }
        public DateTime D_CREATE_DATE { get; set; }
        public DateTime D_MODIF_DATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Puesto, PuestoDTO>()
                .ForMember(dto => dto.I_PUESTO_ID, et => et.MapFrom(a => a.I_ID_PUESTO))
                .ForMember(dto => dto.V_NAME, et => et.MapFrom(a => a.V_NOMBRE))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
