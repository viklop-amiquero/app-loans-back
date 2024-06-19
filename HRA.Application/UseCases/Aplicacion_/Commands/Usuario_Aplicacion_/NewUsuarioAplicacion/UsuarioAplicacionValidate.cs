using FluentValidation;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_Aplicacion_.NewUsuarioAplicacion
{
    public class UsuarioAplicacionValidate : AbstractValidator<NewUsuarioAplicacionVM>
    {
        public UsuarioAplicacionValidate()
        {
            //RuleFor(v => v.I_USER_ID)
            //    .NotEmpty().WithMessage("La llave foránea es requerida")
            //    .Matches(@"^[0-9]+$").WithMessage("La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)");

            //RuleFor(v => v.I_APP_ROL_MENU_ID)
            //    .NotEmpty().WithMessage("La llave foránea es requerida")
            //    .Matches(@"^[0-9]+$").WithMessage("La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)");

            RuleFor(v => v.I_ROLE_ID)
                .NotEmpty().WithMessage("La llave foránea es requerida")
                .Matches(@"^[0-9]+$").WithMessage("La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)");

            //RuleFor(v => v.D_START_DATE)
            //    .NotEmpty().WithMessage("La fecha de inicio es requerida");
            //.Matches(@"^[0-9]+$").WithMessage("La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)");

            //RuleFor(v => v.D_END_DATE)
            //.NotEmpty().WithMessage("La descripción es requerida")
            //.Matches(@"^[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*$").WithMessage("La descripción del rol no es válido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")
        }
    }
}
