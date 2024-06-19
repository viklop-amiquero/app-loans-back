using FluentValidation;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Rol_.NewRol
{
    public class RolValidate : AbstractValidator<NewRolVM>
    {
        public RolValidate() 
        {
            RuleFor(v => v.V_ROLE)
                .NotEmpty().WithMessage("El nombre del rol es requerido.")
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El nombre del rol no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(1, 50).WithMessage("Ingrese un rol con maximo de 50 caracteres.");

            RuleFor(v => v.V_DESCRIPTION)
                .NotEmpty().WithMessage("La descripción del rol es requerida.")
                .Matches(@"^[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*$").WithMessage("La descripción no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(1, 100).WithMessage("Ingrese una descripción con un maximo de 100 caracteres.");
        }
    }
}
