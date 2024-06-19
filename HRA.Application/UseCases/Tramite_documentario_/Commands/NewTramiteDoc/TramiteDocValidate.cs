using FluentValidation;

namespace HRA.Application.UseCases.Tramite_documentario_.Commands.NewTramiteDoc
{
    public class TramiteDocValidate : AbstractValidator<NewTramiteDocVM>
    {
        public TramiteDocValidate()
        {
            RuleFor(v => v.V_NAME)
                .NotEmpty().WithMessage("El nombre de la tasa de interes es requerido.")
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("Los nombre del trámite documentario no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(1, 50).WithMessage("Ingrese como maximo de 200 caracteres.");

            RuleFor(v => v.I_FEE)
                .Matches(@"^(?!.*\s)\d+(\.\d{1,2})?$").WithMessage("La tarifa del trámite documentario no es valido (espacios al inicio o al final, solo debe tener dos decimales, debe ser solo numero).");
   
            RuleFor(v => v.V_DESCRIPTION)
                .Matches("^(?:[^\\s].*)?").WithMessage("La descripción de la tas de interes no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 50).WithMessage("Ingrese como maximo de 50 caracteres.");
        }
    }
}
