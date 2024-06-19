using FluentValidation;

namespace HRA.Application.UseCases.Cuota_.Commands.NewCuota
{
    public class CuotaValidate : AbstractValidator<NewCuotaVM>
    {
        public CuotaValidate()
        {
            // cuota
            RuleFor(v => v.V_ID_CREDIT)
                 .NotEmpty()
                 //.NotEmpty().WithMessage('El ID de la cuenta es obligatorio')
                 .Matches("^[1-9]\\d*$").WithMessage("El ID no puede ser 0 ni números fraccionarios");

            RuleFor(v => v.V_LOAN_AMOUNT)
                .NotEmpty().WithMessage("El monto es requerido")
                .Matches("^(?=.*[1-9])\\d+(\\.\\d{1,2})?$").WithMessage("El monto debe ser mayor a 0 con un máximo de dos digitos despues del punto.");

            RuleFor(v => v.V_PRINCIPAL)
                .NotEmpty().WithMessage("El capital es requerido")
                .Matches("^(?=.*[1-9])\\d+(\\.\\d{1,2})?$").WithMessage("El Capital debe ser mayor 0 con un máximo de dos digitos despues del punto.");

            RuleFor(v => v.V_BALANCE)
                 .NotEmpty().WithMessage("El saldo es requerido")
                .Matches("^(?=.*[0-9])\\d+(\\.\\d{1,2})?$").WithMessage("El saldo puede tener dos digitos como máximo despues del punto.");

            RuleFor(v => v.V_INTEREST)
                .NotEmpty().WithMessage("El capital es requerido")
                .Matches("^(?=.*[1-9])\\d+(\\.\\d{1,2})?$").WithMessage("El interés debe ser mayor a 0 con un máximo de dos digitos despues del punto.");

        }
    }
}
