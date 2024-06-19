using FluentValidation;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.NewUsuario
{
    public class UsuarioValidate : AbstractValidator<NewUsuarioVM>
    {
        public UsuarioValidate()
        {
            RuleFor(v => v.I_PERSON_ID)
                .NotEmpty().WithMessage("La llave foránea es requerido.")
                .Matches(@"^[0-9]+$").WithMessage("El ID de la llave foránea no es válido (letras, caracteres especiales o espacios en blanco");

            RuleFor(v => v.I_ROLE_ID)
                .NotEmpty().WithMessage("La llave foránea es requerida")
                .Matches(@"^[0-9]+$").WithMessage("La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)");

            RuleFor(v => v.D_START_DATE)
                .NotEmpty().WithMessage("La fecha de inicio es requerida");

            RuleFor(v => v.D_END_DATE)
                .NotEmpty().WithMessage("La fecha de fin es requerida");
        }
    }
}
