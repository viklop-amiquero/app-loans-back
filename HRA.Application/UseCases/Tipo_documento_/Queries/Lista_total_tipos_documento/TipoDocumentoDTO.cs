using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Operaciones;

namespace HRA.Application.UseCases.Tipo_documento_.Queries.Lista_total_tipos_documento
{
    public record class TipoDocumentoDTO : IMapFrom<Tipo_documento>
    {
        public int I_DOC_TYPE_ID { get; set; }
        public string? V_ABBREVIATION { get; set; }
        public string V_DOC_NAME { get; set; }
        public int? I_NUMBER_DIGIT { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tipo_documento, TipoDocumentoDTO>()
                .ForMember(dto => dto.I_DOC_TYPE_ID, et => et.MapFrom(a => a.I_ID_TIPO_DOC))
                .ForMember(dto => dto.V_ABBREVIATION, et => et.MapFrom(a => a.V_ABREVIATURA))
                .ForMember(dto => dto.V_DOC_NAME, et => et.MapFrom(a => a.V_NOMBRE_DOC))
                .ForMember(dto => dto.I_NUMBER_DIGIT, et => et.MapFrom(a => a.I_NRO_DIGITOS))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
