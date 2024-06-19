using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Tipo_credito_.Queries.ListaTipoCredito
{
    public record class TiposCreditosDTO : IMapFrom<Tipo_credito>
    {
        public int I_TYPE_CREDIT_ID { get; set; }
        public string V_TYPE_CREDIT { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tipo_credito, TiposCreditosDTO>()
                .ForMember(dto => dto.I_TYPE_CREDIT_ID, et => et.MapFrom(a => a.I_ID_TIPO_CREDITO))
                .ForMember(dto => dto.V_TYPE_CREDIT, et => et.MapFrom(a => a.V_TIPO_CREDITO))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
