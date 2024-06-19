using FluentValidation;

namespace HRA.Application.UseCases.Puesto_.Commands.UpdatePuesto
{
    public class PuestoValidate : AbstractValidator<UpdatePuestoVM>
    {
        public PuestoValidate()
        {
            RuleFor(v => v.V_NAME)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El nombre del puesto no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 50).WithMessage("Ingrese como maximo de 50 caracteres.");

            RuleFor(v => v.V_DESCRIPTION)
                .Matches("^(?:[^\\s].*)?$").WithMessage("La descripción del puesto no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 250).WithMessage("Ingrese como maximo de 250 caracteres.");

        }
    }
}
