using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Tipo_cuenta_.Queries.ListaTipoCuenta
{
    public record class TiposCuentasDTO : IMapFrom<Tipo_cuenta>
    {
        public int I_TYPE_CUENTA_ID { get; set; }
        public string V_TYPE_CUENTA { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tipo_cuenta, TiposCuentasDTO>()
                .ForMember(dto => dto.I_TYPE_CUENTA_ID, et => et.MapFrom(a => a.I_ID_TIPO_CUENTA))
                .ForMember(dto => dto.V_TYPE_CUENTA, et => et.MapFrom(a => a.V_TIPO_CUENTA))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
