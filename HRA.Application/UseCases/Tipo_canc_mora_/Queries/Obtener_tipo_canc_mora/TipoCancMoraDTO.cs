using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Tipo_canc_mora_.Queries.Obtener_tipo_canc_mora
{
    public record class TipoCancMoraDTO : IMapFrom<Tipo_canc_mora>
    {
        public int I_TYPE_CANC_MORA_ID { get; set; }
        public string? V_NAME { get; set; }
        public string? B_STATE { get; set; }
        public int I_USER_CREATE { get; set; }
        public int I_USER_MODIF { get; set; }
        public DateTime D_CREATE_DATE { get; set; }
        public DateTime D_MODIF_DATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tipo_canc_mora, TipoCancMoraDTO>()
                .ForMember(dto => dto.I_TYPE_CANC_MORA_ID, et => et.MapFrom(a => a.I_ID_TIPO_CANC_MORA))
                .ForMember(dto => dto.V_NAME, et => et.MapFrom(a => a.V_NOMBRE))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA));
        }
    }
}
