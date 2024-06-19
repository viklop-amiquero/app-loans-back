using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Puesto_.Commands.ActivatePuesto
{
    public record class ActivatePuestoVM : IRequest<Iresult>
    {
        public int I_POSITION_ID { get; set; }
    }
}
