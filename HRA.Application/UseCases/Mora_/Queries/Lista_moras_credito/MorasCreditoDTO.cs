using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Mora_.Queries.Lista_moras_credito
{
    public record class MorasCreditoDTO : IMapFrom<Mora>
    {
        /// <summary>
        ///  parametros que se envian al front-end
        /// </summary>
        public int I_MORA_ID { get; set; }
        public int I_CUOTA_ID { get; set; }
        public string V_TYPE_MORA { get; set; } = string.Empty;
        public decimal I_MORA_AMOUNT { get; set; }
        public int I_NUMBER_DAYS { get; set; }
        public string? B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Mora, MorasCreditoDTO>()
                .ForMember(dto => dto.I_MORA_ID, et => et.MapFrom(a => a.I_ID_MORA))
                .ForMember(dto => dto.I_CUOTA_ID, et => et.MapFrom(a => a.I_ID_CUOTA))
                .ForMember(dto => dto.I_MORA_AMOUNT, et => et.MapFrom(a => a.I_MONTO_MORA))
                .ForMember(dto => dto.I_NUMBER_DAYS, et => et.MapFrom(a => a.I_NUMERO_DIA))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
