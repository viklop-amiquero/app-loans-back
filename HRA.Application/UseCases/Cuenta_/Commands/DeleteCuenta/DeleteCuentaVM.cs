using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cuenta_.Commands.DeleteCuenta
{
    public record class DeleteCuentaVM : IRequest<Iresult>
    {
        public string V_NUMBER_ACCOUNT { get; set; }
    }
}
