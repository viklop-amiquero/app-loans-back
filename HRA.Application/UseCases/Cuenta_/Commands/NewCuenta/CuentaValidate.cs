using FluentValidation;

namespace HRA.Application.UseCases.Cuenta_.Commands.NewCuenta
{
    public class CuentaValidate : AbstractValidator<NewCuentaVM>
    {
        public CuentaValidate() 
        {

            RuleFor(v => v.I_PERSON_ID)
                .NotEmpty().WithMessage("El ID del cliente es obligatorio")
                .Must(id => id > 0).WithMessage("El ID del cliente debe ser un número entero positivo y no puede ser 0");

            RuleFor(v => v.V_TYPE_ACCOUNT_ID)
               .NotEmpty().WithMessage("El tipo de cuenta es obligatorio.")
               .Matches(@"^[0-9,]+(?:,[0-9,]+)*$").WithMessage("El tipo decuenta no es válido (letras, caracteres especiales, espacios o debe contener solo comas y numeros ejemplo:1,2,3 ).");

        }
    }
}
