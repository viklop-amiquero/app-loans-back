using FluentValidation;
using HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.NewAplicacionRolMenu;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.UpdateAplicacionRolMenu
{
    public class AplicacionRolMenuValidate : AbstractValidator<NewAplicacionRolMenuVM>
    {
        public AplicacionRolMenuValidate()
        {
            RuleFor(v => v.I_PERMISSION_ID);

        }
    }
}
