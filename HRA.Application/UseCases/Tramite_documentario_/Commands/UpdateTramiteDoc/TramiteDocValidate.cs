using FluentValidation;

namespace HRA.Application.UseCases.tramite_documentario_.Commands.UpdateTramiteDoc
{
    public class TramiteDocValidate : AbstractValidator<UpdateTramiteDocVM>
    {
        public TramiteDocValidate()
        {

            RuleFor(v => v.V_PROCEDURE_DOC_ID)
               .Matches("^[1-9]\\d*$").WithMessage("El ID no puede ser 0 ni números fraccionarios");

            RuleFor(v => v.V_NAME)
                .Matches(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El nombre del trámite documentario no es válido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")
                .Length(0, 50).WithMessage("Ingrese como maximo de 50 caracteres.");

            RuleFor(v => v.V_FEE)
                .Matches("^(?!.*\\s)\\d*(\\.\\d{1,2})?$")
                .WithMessage("El tasa de interés debe tener como máximo dos digitos después del punto decimal");

            RuleFor(v => v.V_DESCRIPTION)
                .Matches("^(?:[^\\s].*)?$").WithMessage("La descripción no es válida (espacios al inicio o al final de la data de entrada)")
                .Length(0, 100).WithMessage("Ingrese como maximo de 100 caracteres.");

        }
    }
}