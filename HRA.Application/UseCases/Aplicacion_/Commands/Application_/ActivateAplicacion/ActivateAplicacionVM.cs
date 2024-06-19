using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Application_.ActivateAplicacion
{
    public record class ActivateAplicacionVM : IRequest<Iresult>
    {
        public int I_APLICATION_ID { get; set; }
    }
}
