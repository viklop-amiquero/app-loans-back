using FluentValidation;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Application_.UpdateAplicacion
{
    public class AplicacionValidate : AbstractValidator<UpdateAplicacionVM>
    {
        public AplicacionValidate()
        {
            RuleFor(v => v.V_APLICATION)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El nombre del aplicativo no es válido (doble espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")
                .Length(0, 200).WithMessage("Ingrese un nombre con un máximo de 200 caracteres.");

            RuleFor(v => v.V_ACRONYM)
                .Matches(@"^(?:[A-Za-z]+(?:[-_ ]?[A-Za-z]+)*)?$").WithMessage("El acronimo no es válido (espacios al inicio o al final de la data de entrada)")
                .Length(0, 20).WithMessage("Ingrese un acronimo con un máximo de 20 caracteres.");

            RuleFor(v => v.V_DESCRIPTION)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ0-9]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ0-9]+)*)?$").WithMessage("La descripción del aplicativo no es válido (espacios al inicio o al final de la data de entrada)")
                .Length(0, 200).WithMessage("Ingrese una descripción con un máximo de 200 caracteres.");

            RuleFor(v => v.V_URL)
                .Matches(@"^(?:[^\s].*[^\s])?$").WithMessage("La URL del aplicativo no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 250).WithMessage("Ingrese una URL con un máximo de 250 caracteres.");
        }
    }
}
