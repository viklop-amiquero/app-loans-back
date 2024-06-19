using FluentValidation;

namespace HRA.Application.UseCases.Cuenta_.Commands.UpdateCuenta
{
    public class CuentaValidate : AbstractValidator<UpdateCuentaVM>
    {
        public CuentaValidate() 
        {

            RuleFor(v => v.V_ID_ACCOUNT_TYPE)
               .Matches("^[1-9]\\d*$").WithMessage("El ID no puede ser 0 ni números fraccionarios");
               

            RuleFor(v => v.V_ACCOUNT_NUMBER)
                .Matches("^$|^(null|\\d{4,12})$")
                .WithMessage("El número de cuenta debe contener solo números y mínimo 4 digitos.");

            RuleFor(v => v.V_BALANCE)
                .Matches("^$|^(null|\\d+(\\.\\d{1,2})?)$")
                .WithMessage("El saldo debe tener exactamente dos decimales");

        }
    }
}
