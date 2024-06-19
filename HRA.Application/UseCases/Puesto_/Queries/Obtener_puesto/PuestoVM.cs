using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Puesto_.Queries.Obtener_puesto
{
    public record class PuestoVM : IRequest<Iresult>
    {
        public string? I_PUESTO_ID { get; set; }
    }
}
