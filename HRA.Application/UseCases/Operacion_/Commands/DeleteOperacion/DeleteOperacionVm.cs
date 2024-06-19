using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Operacion_.Commands.DeleteOperacion
{
    public record class DeleteOperacionVM : IRequest<Iresult>
    {
        public int I_OPERACION_ID { get; set; }
    }
}
