using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Cuenta_.Commands.UpdateCuenta
{
    public record class UpdateCuentaVM : IRequest<Iresult>
    {
        public int I_ID_ACCOUNT { get; set; }
        public string V_ID_ACCOUNT_TYPE { get; set; }
        public string V_BALANCE { get; set; }
        public string V_ACCOUNT_NUMBER { get; set; }
    }
}
