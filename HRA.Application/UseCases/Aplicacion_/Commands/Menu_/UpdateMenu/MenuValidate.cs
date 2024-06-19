using FluentValidation;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Menu_.UpdateMenu
{
    public class MenuValidate : AbstractValidator<UpdateMenuVM>
    {
        public MenuValidate()
        {
            RuleFor(v => v.V_NAME)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El menú no es válido (doble espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")
                .Length(0, 50).WithMessage("Ingrese un menú con un máximo de 50 caracteres.");

            RuleFor(v => v.V_DESCRIPTION)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("La descripción del menú no es válido (mas de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")
                .Length(0, 100).WithMessage("Ingrese una descripción con un máximo de 100 caracteres.");

            RuleFor(v => v.V_ICON)
                .Matches(@"^(?:[^\s].*[^\s])?$").WithMessage("El ícono no es válido (espacios al inicio o al final de la data de entrada)")
                .Length(0, 100).WithMessage("Ingrese una descripción con un máximo de 100 caracteres.");

            RuleFor(v => v.V_ROUTE)
                .Matches(@"^(?:[^\s].*[^\s])?$").WithMessage("La ruta no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 50).WithMessage("Ingrese una descripción con un máximo de 50 caracteres.");

            RuleFor(v => v.V_URL)
                .Matches(@"^(?:[^\s].*[^\s])?$").WithMessage("La URL no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 100).WithMessage("Ingrese una URL con un máximo de 100 caracteres.");
        }
    }
}
