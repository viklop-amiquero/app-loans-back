using FluentValidation;

namespace HRA.Application.UseCases.Mora_.Commands.UpdateMora
{
    public class MoraValidate : AbstractValidator<UpdateMoraVM>
    {
        public MoraValidate()
        {
            RuleFor(v => v.I_TYPE_CANC_MORA_ID)
                .Matches(@"^$|^[0-9]+$").WithMessage("La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)");

            RuleFor(v => v.I_AMOUNT_MORA)
                .Matches(@"^([0-9]*\.?[0-9]+)?$").WithMessage("El monto de la mora no es válido (letras, caracteres especiales o espacios en blanco)");
        }
    }
}
