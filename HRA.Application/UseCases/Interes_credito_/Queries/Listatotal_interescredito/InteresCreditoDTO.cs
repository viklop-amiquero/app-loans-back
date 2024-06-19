using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Interes_credito_.Queries.Listatotal_interescredito
{
    public record class InteresCreditoDTO : IMapFrom<Interes_credito>
    {
        public int I_INTEREST_CREDIT_ID { get; set; }
        public string V_NAME { get; set; }
        public decimal I_INTEREST { get; set; }
        public string V_FREQUENCY { get; set; }
        public string? V_DESCRIPTION { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Interes_credito, InteresCreditoDTO>()
                .ForMember(dto => dto.I_INTEREST_CREDIT_ID, et => et.MapFrom(a => a.I_ID_INTERES_CREDITO))
                .ForMember(dto => dto.V_NAME, et => et.MapFrom(a => a.V_NOMBRE))
                .ForMember(dto => dto.I_INTEREST, et => et.MapFrom(a => a.I_TASA_INTERES))
                .ForMember(dto => dto.V_FREQUENCY, et => et.MapFrom(a => a.V_FRECUENCIA))
                .ForMember(dto => dto.V_DESCRIPTION, et => et.MapFrom(a => a.V_DESCRIPCION))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
