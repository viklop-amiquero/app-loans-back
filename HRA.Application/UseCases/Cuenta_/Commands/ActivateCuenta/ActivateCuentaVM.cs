using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cuenta_.Commands.ActivateCuenta
{
    public record class ActivateCuentaVM : IRequest<Iresult>
    {
        public string V_NUMBER_ACCOUNT { get; set; }
    }
}
