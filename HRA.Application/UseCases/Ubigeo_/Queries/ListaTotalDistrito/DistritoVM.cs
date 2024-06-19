using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Ubigeo_.Queries.ListaTotalDistrito
{ 
    public record class DistritoVM : IRequest<Iresult>
    {
        public string? V_PROVINCE_CODE { get; set; }
    }
}
