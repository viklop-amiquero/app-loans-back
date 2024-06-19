using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Application_.DeleteAplicacion
{
    public record class DeleteAplicacionVM : IRequest<Iresult>
    {
        public int I_APLICATION_ID { get; set; }
    }
}
