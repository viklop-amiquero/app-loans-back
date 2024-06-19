using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.tramite_documentario_.Commands.UpdateTramiteDoc
{
    public record class UpdateTramiteDocVM : IRequest<Iresult>
    {
        public string V_PROCEDURE_DOC_ID { get; set; }
        public string V_NAME { get; set; }
        public string V_FEE { get; set; }
        public string? V_DESCRIPTION { get; set; }
    }
}
