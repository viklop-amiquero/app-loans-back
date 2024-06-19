using FluentValidation;

namespace HRA.Application.UseCases.Credito_.Commands.UpdateCredito
{
    public class CreditoValidate : AbstractValidator<UpdateCreditoVM>
    {
        public CreditoValidate() 
        {
            RuleFor(v => v.V_INTEREST_CREDIT_ID)
               .Matches("^[1-9]\\d*$").WithMessage("El ID no puede ser 0 ni números fraccionarios")
               .WithMessage("El id tipo crédito debe contener solo números enteros.");

            RuleFor(v => v.V_ID_TYPE_CREDIT)
               .Matches("^[1-9]\\d*$").WithMessage("El ID no puede ser 0 ni números fraccionarios")
               .WithMessage("El id tipo crédito debe contener solo números enteros.");

            RuleFor(v => v.V_LOAN_AMOUNT)
                .Matches("^$|^(null|\\d+(\\.\\d{1,2})?)$")
                .WithMessage("El monto de préstamo debe tener exactamente dos decimales");


            RuleFor(v => v.V_PAYMENT_FREQUENCY)
                .Matches("^$|^null$|^[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)?$")
                .WithMessage("La frecuencia de pago es incorrecta")
                .Length(0, 25);

            RuleFor(v => v.V_TERM_QUANTITY)
                .Matches("^$|^(null|\\b(?:[1-9]|[1-5]\\d|60)?\\b)?$")
                .WithMessage("El plazo debe ser mayor a 1 y menor a 60");


            RuleFor(v => v.V_DAY_PAY)
                .Matches("^$|^(null|([1-9]|[12]\\d|3[01])?)$")
                .WithMessage("El dia de pago debe ser mayor a 1 y menor a 31");

        }
    }
}
