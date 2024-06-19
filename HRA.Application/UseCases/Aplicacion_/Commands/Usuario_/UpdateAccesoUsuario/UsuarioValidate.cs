using FluentValidation;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.UpdateAccesoUsuario
{
    public class UsuarioValidate : AbstractValidator<UpdateAccesoUsuarioVM>
    {
        public UsuarioValidate()
        {
            RuleFor(v => v.D_END_DATE)
                .NotEmpty().WithMessage("La fecha de fin es requerida");
        }
    }
}
