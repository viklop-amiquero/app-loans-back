using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Tipo_mora_.Queries.ListaTipoMora
{
    public record class TiposMorasDTO :IMapFrom<Tipo_mora>
    {
        public int I_ID_TYPE_MORA { get; set; }
        public string V_NAME { get; set; }
        public decimal I_AMOUNT { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tipo_mora, TiposMorasDTO>()
                .ForMember(dto => dto.I_ID_TYPE_MORA, et => et.MapFrom(a => a.I_ID_TIPO_MORA))
                .ForMember(dto => dto.V_NAME, et => et.MapFrom(a => a.V_NOMBRE))
                .ForMember(dto => dto.I_AMOUNT, et => et.MapFrom(a => a.I_MONTO))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));

        }
    }
}
