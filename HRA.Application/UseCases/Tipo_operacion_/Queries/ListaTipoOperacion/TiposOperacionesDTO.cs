using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Tipo_operacion_.Queries.ListaTipoOperacion
{
    public record class TiposOperacionesDTO : IMapFrom<Tipo_operacion>
    {
        public int I_TYPE_OPERATION_ID { get; set; }
        public string V_IDENTIFIER { get; set; }
        public string V_TYPE_OPERATION { get; set; }
        public string? V_DESCRIPTION { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tipo_operacion, TiposOperacionesDTO>()
                .ForMember(dto => dto.I_TYPE_OPERATION_ID, et => et.MapFrom(a => a.I_ID_TIPO_OPERACION))
                .ForMember(dto => dto.V_IDENTIFIER, et => et.MapFrom(a => a.V_IDENTIFICADOR))
                .ForMember(dto => dto.V_TYPE_OPERATION, et => et.MapFrom(a => a.V_TIPO_OPERACION))
                .ForMember(dto => dto.V_DESCRIPTION, et => et.MapFrom(a => a.V_DESCRIPCION))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
