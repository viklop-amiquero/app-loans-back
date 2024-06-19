using FluentValidation;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Rol_.UpdateRol
{
    public class RolValidate : AbstractValidator<UpdateRolVM>
    {
        public RolValidate() 
        {
            RuleFor(v => v.V_ROLE)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El nombre del rol no es válido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")
                .Length(0, 50).WithMessage("Ingrese un nombre con maximo de 50 caracteres.");

            RuleFor(v => v.V_DESCRIPTION)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("La descripción del rol no es válido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")
                .Length(0, 100).WithMessage("Ingrese un nombre con maximo de 100 caracteres.");
        }
    }
}
