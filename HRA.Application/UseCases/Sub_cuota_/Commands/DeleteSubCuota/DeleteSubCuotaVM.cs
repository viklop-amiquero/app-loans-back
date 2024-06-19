using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Sub_cuota_.Commands.DeleteSubCuota
{
    public record class DeleteSubCuotaVM : IRequest<Iresult>
    {
        public int I_SUB_CUOTA_ID { get; set; }
    }
}
