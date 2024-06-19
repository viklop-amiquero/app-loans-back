using FluentValidation;

namespace HRA.Application.UseCases.Credito_.Commands.NewCredito
{
    public class CreditoValidate : AbstractValidator<NewCreditoVM>
    {
        public CreditoValidate() 
        {
            RuleFor(v => v.I_PERSON_ID)
                 .NotEmpty().WithMessage("El ID del cliente es obligatorio")
                 .Must(id => id > 0).WithMessage("El ID debe ser un número entero positivo y no puede ser 0");

            RuleFor(v => v.V_ID_TYPE_CREDIT)
                .NotEmpty().WithMessage("El tipo de crédito es requerido")
                .Matches("^[1-9]\\d*$").WithMessage("El ID no puede ser 0 ni números fraccionarios");

            RuleFor(v => v.V_LOAN_AMOUNT)
                .NotEmpty().WithMessage("El monto es requerido")
                .Matches("^(?=.*[1-9])\\d+(\\.\\d{1,2})?$").WithMessage("El monto debe ser mayor con un máximo de dos digitos despues del punto.");

            RuleFor(v => v.V_ID_PAYMENT_FREQUENCY)
                .NotEmpty().WithMessage("La llave foránea de de  es requerido.")
                .Matches(@"^[0-9]+$").WithMessage("El ID de la llave foranes no es válido (letras, caracteres especiales o espacios).");

            RuleFor(v => v.V_TERM_QUANTITY)
                .NotEmpty().WithMessage("El plazo es requerido")
                .Matches("^\\d{1,6}$").WithMessage("El plazo debe ser mínimo de 1 y máximo 6 dígitos");

            RuleFor(v => v.I_INTEREST_CREDIT_ID)
                .NotEmpty().WithMessage("El ID de interes de crédito es obligatorio")
                .Must(id => id > 0).WithMessage("El ID de interes de crédito debe ser un número entero positivo y no puede ser 0");

        }
    }
}
