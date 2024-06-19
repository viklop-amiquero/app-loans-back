using FluentValidation;

namespace HRA.Application.UseCases.Operacion_.Commands.NewOperacion
{
    public class OperacionValidate : AbstractValidator<NewOperacionVM>
    {
        public OperacionValidate()
        {
            RuleFor(v => v.V_ID_ACCOUNT)
                .NotEmpty()
                //.NotEmpty().WithMessage('El ID de la cuenta es obligatorio')
                .Matches("^[1-9]\\d*$").WithMessage("El ID no puede ser 0 ni números fraccionarios");

            //RuleFor(v => v.V_ID_CUOTA)
            //    .NotEmpty()
            //    .NotEmpty().WithMessage('El ID de la cuenta es obligatorio')
            //    .Matches("^[1-9]\\d*$").WithMessage("El ID no puede ser 0 ni números fraccionarios")
            //    .Length(0, 20);

            RuleFor(v => v.V_ID_TYPE_OPERATION)
                .NotEmpty().WithMessage("El tipo de operación es requerido")
                .Matches("^[1-9]\\d*$").WithMessage("El ID no puede ser 0 ni números fraccionarios");

            RuleFor(v => v.V_AMOUNT)
                .NotEmpty().WithMessage("El monto es requerido")
                .Matches("^(?=.*[1-9])\\d+(\\.\\d{1,2})?$").WithMessage("El monto debe ser mayor con un máximo de dos digitos despues del punto.");

            //RuleFor(v => v.V_NUMBER_OPERATION)
            //    .NotEmpty().WithMessage("El número de operación es requerido")
            //    .Matches("^[0-9]{1,10}$").WithMessage("El número de operación debe contener entre 1 y 10 dígitos numéricos")
            //    .Length(1, 10).WithMessage("El número de operación debe tener entre 1 y 10 caracteres");



        }
    }
}
