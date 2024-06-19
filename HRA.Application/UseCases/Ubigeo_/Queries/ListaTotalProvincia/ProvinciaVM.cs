using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Ubigeo_.Queries.ListaTotalProvincia
{
    public record class ProvinciaVM : IRequest<Iresult>
    {
        public string? V_DEPARTAMENT_CODE { get; set; }
    }
}
