using FluentValidation;

namespace HRA.Application.UseCases.Tipo_documento_.Commands.UpdateTipoDocumento
{
    public class TipoDocumentoValidate : AbstractValidator<UpdateTipoDocumentoVM>
    {
        public TipoDocumentoValidate() 
        {
            RuleFor(v => v.V_ABBREVIATION)
                .Matches("^(?:[^\\s]+(?:[a-zA-Z/. ]*[^\\s])?)?$").WithMessage("La abreviatura del tipo de documento no es válida (tildes o caracteres entre palabras que no sean '/, ')")
                .Length(0, 20).WithMessage("La abreviatura del tipo de documento debe contener un máximo de 20 caracteres.");

            RuleFor(v => v.V_DOC_NAME)
                .Matches("^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$").WithMessage("El tipo de documento no es válido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")
                .Length(0, 100).WithMessage("El tipo de documento debe contener un máximo de 100 caracteres.");

            RuleFor(v => v.I_DIGITS_NUMBER)
                .Matches("^(null|[0-9]{0,2})$").WithMessage("El número de dígitos del tipo de documento no es válido (letras, caracteres especiales o espacios en blanco)");
        }
    }
}
