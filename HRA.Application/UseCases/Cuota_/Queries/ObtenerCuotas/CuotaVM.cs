using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cuota_.Queries.ObtenerCuotas
{
    public record class CuotaVM : IRequest<Iresult>
    {
        public int I_INSTALLMENT_ID { get; set; }
    }
}
