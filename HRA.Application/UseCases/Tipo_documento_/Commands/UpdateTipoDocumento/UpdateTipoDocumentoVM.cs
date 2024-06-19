using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Tipo_documento_.Commands.UpdateTipoDocumento
{
    public record class UpdateTipoDocumentoVM : IRequest<Iresult>
    {
        public int I_DOC_TYPE_ID { get; set; }

        [RegularExpression(@"^(?:[^\s]+(?:[a-zA-Z/. ]*[^\s])?)?$", ErrorMessage = "La abreviatura del tipo de documento no es válida (tildes o caracteres entre palabras que no sean '/, ')")]
        public string? V_ABBREVIATION { get; set; }

        [RequiredNull(ErrorMessage = "El tipo de documento es requerido")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El tipo de documento no es válido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")]
        public string V_DOC_NAME { get; set; }

        [RegularExpression(@"^(null|[0-9]{0,2})$", ErrorMessage = "El número de dígitos del tipo de documento no es válido (letras, caracteres especiales o espacios en blanco)")]
        public string? I_DIGITS_NUMBER { get; set; }
    }
}
