using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cuenta_.Queries.Lista_total_cuentas
{
    public record class CuentasVM : IRequest<Iresult>
    {
    }
}
