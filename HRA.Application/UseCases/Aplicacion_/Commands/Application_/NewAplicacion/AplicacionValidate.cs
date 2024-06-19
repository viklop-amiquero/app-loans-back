using FluentValidation;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Application_.NewAplicacion
{
    public class AplicacionValidate : AbstractValidator<NewAplicacionVM>
    {
        public AplicacionValidate()
        {
            RuleFor(v => v.V_APLICATION)
                .NotEmpty().WithMessage("El nombre del aplicativo es requerido.")
                .Matches(@"^[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*$").WithMessage("El nombre del aplicativo no es válido (mas de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")
                .Length(1, 200).WithMessage("Ingrese un nombre con un máximo de 200 caracteres.");

            RuleFor(v => v.V_ACRONYM)
                .Matches(@"^(?:[A-Za-z]+(?:[-_ ]?[A-Za-z]+)*)$").WithMessage("El acrónimo no es válido (mas de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")
                .Length(0, 20).WithMessage("Ingrese un acrónimo con un máximo de 20 caracteres.");

            RuleFor(v => v.V_DESCRIPTION)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("La descripción del aplicativo no es válida (mas de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")
                .Length(0, 200).WithMessage("Ingrese una descripción con un máximo de 200 caracteres.");

            RuleFor(v => v.V_URL)
                .Matches(@"^(?:[^\s].*[^\s])?$").WithMessage("La URL del aplicativo no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 250).WithMessage("Ingrese una URL con un máximo de 250 caracteres.");
        }
    }
}
