using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Tipo_documento_.Commands.ActivateTipoDocumento
{
    public record class ActivateTipoDocumentoVM : IRequest<Iresult>
    {
        public int I_DOC_TYPE_ID { get; set; }
    }
}
