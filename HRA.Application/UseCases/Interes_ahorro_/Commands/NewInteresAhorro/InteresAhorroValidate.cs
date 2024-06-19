using FluentValidation;

namespace HRA.Application.UseCases.Interes_ahorro_.Commands.NewInteresAhorro
{
    public class TasaInteresValidate : AbstractValidator<NewInteresAhorroVM>
    {
        public TasaInteresValidate()
        {

            RuleFor(v => v.V_NAME)
                .NotEmpty().WithMessage("El nombre de la tasa de interes es requerido.")
                .Matches(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]+$").WithMessage("El nombre de la tasa de interes no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(1, 50).WithMessage("Ingrese como maximo de 50 caracteres.");

            RuleFor(v => v.V_FREQUENCY)
                .Matches(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$").WithMessage("La frecuencia no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 20).WithMessage("Ingrese como maximo de 25 caracteres.");
           
            RuleFor(v => v.V_DESCRIPTION)
                .Matches("^(?:[^\\s].*)?").WithMessage("La descripción de la tas de interes no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 50).WithMessage("Ingrese como maximo de 50 caracteres.");
        }
    }
}
