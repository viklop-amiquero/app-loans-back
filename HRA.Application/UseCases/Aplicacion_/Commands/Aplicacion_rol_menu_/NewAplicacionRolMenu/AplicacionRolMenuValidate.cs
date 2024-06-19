using FluentValidation;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.NewAplicacionRolMenu
{
    public class AplicacionRolMenuValidate : AbstractValidator<NewAplicacionRolMenuVM>
    {
        public AplicacionRolMenuValidate()
        {
            RuleFor(v => v.I_MENU_ID)
                .NotEmpty().WithMessage("La llave foránea es requerida");

            RuleFor(v => v.I_ROLE_ID)
                .NotEmpty().WithMessage("La llave foránea es requerida")
                .Matches(@"^[0-9]+$").WithMessage("La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)");

            RuleFor(v => v.I_PERMISSION_ID)
                .NotEmpty().WithMessage("La llave foránea es requerida");
        }
    }
}
