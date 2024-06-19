using FluentValidation;

namespace HRA.Application.UseCases.Login_.Queries.Autentication
{
    public class Validate : AbstractValidator<LoginVM>
    {
        public Validate()
        {
            RuleFor(v => v.V_USER)
                .NotEmpty().WithMessage("El documento de identidad es requerido.")
                .Matches(@"^[0-9]{8,13}$").WithMessage("Ingrese un usuario válido.");

            RuleFor(v => v.V_PASSWORD)
                .NotEmpty().WithMessage("El password es requerido.");
                //.Length(8, 80).WithMessage("El password debe tener entre 8 y 80 caracteres.");

            //RuleFor(v => v.V_IP)
            //    .NotEmpty().WithMessage("La IP es requerida.");
        }
    }
}
