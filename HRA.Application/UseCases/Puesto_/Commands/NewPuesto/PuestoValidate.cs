using FluentValidation;

namespace HRA.Application.UseCases.Puesto_.Commands.NewPuesto
{
    public class PuestoValidate : AbstractValidator<NewPuestoVM>
    {
        public PuestoValidate()
        {
            RuleFor(v => v.V_NAME)
                .NotEmpty().WithMessage("El nombre del puesto es requerido.")
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El nombvre del puesto no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(1, 50).WithMessage("Ingrese como maximo de 50 caracteres.");
            
            RuleFor(v => v.V_DESCRIPTION)
                .Matches(@"^[^\s].*[^\s]$").WithMessage("La descripción del puesto no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 200).WithMessage("Ingrese como maximo de 200 caracteres.");
            
        }
    }
}
