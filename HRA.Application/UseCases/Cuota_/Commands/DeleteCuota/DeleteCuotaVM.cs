using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cuota_.Commands.DeleteCuota
{
    public record class DeleteCuotaVM : IRequest<Iresult>
    {
        public int I_CUOTA_ID { get; set; }
    }
}
