using FluentValidation;

namespace HRA.Application.UseCases.Cuota_.Commands.UpdateCuota
{
    public class CuotaValidate : AbstractValidator<UpdateCuotaVM>
    {
        public CuotaValidate() 
        {

            RuleFor(v => v.V_ID_CREDIT)
                 .NotEmpty()
                 //.NotEmpty().WithMessage('El ID de la cuenta es obligatorio')
                 .Matches("^[1-9]\\d*$").WithMessage("El ID no puede ser 0 ni números fraccionarios");

            RuleFor(v => v.V_LOAN_INSTALLMENT)
                .Matches("^$|^(null|\\d+(\\.\\d{1,2})?)$").WithMessage("El monto de préstamo puede tener como máximo dos decimales después del punto.");

            RuleFor(v => v.V_PRINCIPAL)
                .Matches("^$|^(null|\\d+(\\.\\d{1,2})?)$").WithMessage("El Capital debe ser mayor 0 con un máximo de dos digitos despues del punto.");

            RuleFor(v => v.V_BALANCE)
                .Matches("^$|^(?=.*[0-9])\\d+(\\.\\d{1,2})?$").WithMessage("El saldo puede tener dos digitos como máximo despues del punto.");

            RuleFor(v => v.V_INTEREST)
                .Matches("^$|^(?=.*[0-9])\\d+(\\.\\d{1,2})?$").WithMessage("El interés debe ser mayor a 0 con un máximo de dos digitos despues del punto.");

        }
    }
}
