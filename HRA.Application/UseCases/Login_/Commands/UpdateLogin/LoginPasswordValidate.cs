using FluentValidation;

namespace HRA.Application.UseCases.Login_.Commands.UpdateLogin
{
    public class LoginPasswordValidate : AbstractValidator<UpdateLoginVM>
    {
        public LoginPasswordValidate()
        {
            //RuleFor(v => v.V_ID_USER)
            //    .NotEmpty().WithMessage("El ID es requerido.")
            //    .Matches(@"^[0-9]{1,8}$").WithMessage("Ingrese un ID válido.");

            //RuleFor(v => v.V_USUARIO)
            //    .NotEmpty().WithMessage("El documento de identidad es requerido.")
            //    .Matches(@"^[0-9]{8,13}$").WithMessage("Ingrese un usuario válido.");

            RuleFor(v => v.V_PASSWORD)
                .NotEmpty().WithMessage("La contraseña es requerida.");
                //.Length(8, 80).WithMessage("El password debe tener entre 8 y 80 caracteres.");

            RuleFor(v => v.V_NEW_PASSWORD)
                .NotEmpty().WithMessage("La nueva contraseña es requerida.")
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*]).{8,16}$").WithMessage("Ingrese una contraseña que contenga como mínimo una letra mayúscula, una letra minúscula, un dígito y un carácter especial.")
                .Length(1, 16).WithMessage("Ingrese una contraseña con un maximo de 16 caracteres.");
            //.Equal(v => v.V_CONFIRM_PASSWORD).WithMessage("No coincide el nuevo password con la confirmación.");

            //RuleFor(v => v.V_CONFIRM_PASSWORD)
            //    .NotEmpty().WithMessage("La confirmación del password es requerida.")
            //    .Matches(@"^(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.*[0-9]).{8,13}$").WithMessage("Ingrese un password que contenga como mínimo una mayúscula, un número y un símbolo.")
            //    .Equal(v => v.V_NEW_PASSWORD).WithMessage("No coincide el password con la confirmación.");
        }
    }
}
