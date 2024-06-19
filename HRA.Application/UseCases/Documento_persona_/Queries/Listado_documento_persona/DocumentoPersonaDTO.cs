using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.Registro;

namespace HRA.Application.UseCases.Documento_persona_.Queries.Listado_documento_persona
{
    public record class DocumentoPersonaDTO : IMapFrom<Documento_persona>
    {
        public int I_TIPO_DOC_ID { get; set; }
        public int I_PERSONA_ID { get; set; }
        public string V_NRO_DOCUMENTO { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Documento_persona, DocumentoPersonaDTO>()
                .ForMember(dto => dto.I_TIPO_DOC_ID, et => et.MapFrom(a => a.I_ID_TIPO_DOC))
                .ForMember(dto => dto.I_PERSONA_ID, et => et.MapFrom(a => a.I_ID_PERSONA))
                .ForMember(dto => dto.V_NRO_DOCUMENTO, et => et.MapFrom(a => a.V_NRO_DOCUMENTO))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
