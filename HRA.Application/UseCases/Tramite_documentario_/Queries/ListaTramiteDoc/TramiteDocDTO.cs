using AutoMapper;
using HRA.Application.Common.Mappings;
using HRA.Domain.Entities.RapiDiario;

namespace HRA.Application.UseCases.Tramite_documentario_.Queries.ListaTramiteDoc
{
    public record class TramiteDocDTO : IMapFrom<Tramite_documentario>
    {
        public int I_PROCEDURE_DOC_ID { get; set; }
        public string V_NAME { get; set; }
        public decimal I_FEE { get; set; }
        public string? V_DESCRIPTION { get; set; }
        public string B_STATE { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Tramite_documentario, TramiteDocDTO>()
                .ForMember(dto => dto.I_PROCEDURE_DOC_ID, et => et.MapFrom(a => a.I_ID_TRAMITE_DOC))
                .ForMember(dto => dto.V_NAME, et => et.MapFrom(a => a.V_NOMBRE))
                .ForMember(dto => dto.I_FEE, et => et.MapFrom(a => a.I_TARIFA))
                .ForMember(dto => dto.V_DESCRIPTION, et => et.MapFrom(a => a.V_DESCRIPCION))
                .ForMember(dto => dto.B_STATE, et => et.MapFrom(a => a.B_ESTADO));
        }
    }
}
