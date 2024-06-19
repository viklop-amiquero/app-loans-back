using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Mora_.Queries.ObtenerMora
{
    public record class MoraVM : IRequest<Iresult>
    {
        public string? V_CUOTA_ID { get; set; }
    }
}
