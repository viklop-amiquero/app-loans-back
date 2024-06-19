using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Cancelacion_mora_.Queries.ObtenerCancelacionMora
{
    public record class CancelacionMoraDTO : IMapFrom<Cancelacion_mora>
    {
        public int I_CANC_MORA_ID { get; set; }
        public int I_MORA_ID { get; set; }
        public int I_TYPE_CANC_MORA_ID { get; set; }
        public decimal I_AMOUNT_CANC_MORA { get; set; }
        public decimal I_START_AMOUNT_MORA { get; set; }
        public decimal I_END_AMOUNT_MORA { get; set; }
        public string? B_STATE { get; set; }
        public int I_USER_CREATE { get; set; }
        public int I_USER_MODIF { get; set; }
        public DateTime D_CREATE_DATE { get; set; }
        public DateTime D_MODIF_DATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Cancelacion_mora, CancelacionMoraDTO>()
                .ForMember(dto => dto.I_CANC_MORA_ID, et => et.MapFrom(a => a.I_ID_CANC_MORA))
                .ForMember(dto => dto.I_MORA_ID, et => et.MapFrom(a => a.I_ID_MORA))
                .ForMember(dto => dto.I_TYPE_CANC_MORA_ID, et => et.MapFrom(a => a.I_ID_TIPO_CANC_MORA))
                .ForMember(dto => dto.I_AMOUNT_CANC_MORA, et => et.MapFrom(a => a.I_MONTO_CANC_MORA))
                .ForMember(dto => dto.I_START_AMOUNT_MORA, et => et.MapFrom(a => a.I_MONTO_INICIAL_MORA))
                .ForMember(dto => dto.I_END_AMOUNT_MORA, et => et.MapFrom(a => a.I_MONTO_FINAL_MORA))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO))
                .ForMember(dto => dto.I_USER_CREATE, et => et.MapFrom(a => a.I_USUARIO_CREACION))
                .ForMember(dto => dto.D_CREATE_DATE, et => et.MapFrom(a => a.D_FECHA_CREACION))
                .ForMember(dto => dto.I_USER_MODIF, et => et.MapFrom(a => a.I_USUARIO_MODIFICA))
                .ForMember(dto => dto.D_MODIF_DATE, et => et.MapFrom(a => a.D_FECHA_MODIFICA));
        }
    }
}
