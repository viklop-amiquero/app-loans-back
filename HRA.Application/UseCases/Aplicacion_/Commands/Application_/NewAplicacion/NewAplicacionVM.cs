using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Application_.NewAplicacion
{
    public record class NewAplicacionVM : IRequest<Iresult>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del aplicativo es requerido")]
        [RegularExpression(@"^[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*$", ErrorMessage = "El nombre del aplicativo no es válido (mas de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")]
        [RequiredNull(ErrorMessage = "El nombre del aplicativo es requerido")]
        public string V_APLICATION { get; set; } = string.Empty;

        [RegularExpression(@"^(?:[A-Za-z]+(?:[-_ ]?[A-Za-z]+)*)$", ErrorMessage = "El acrónimo del aplicativo no es válido")]
        public string? V_ACRONYM { get; set; }

        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "La descripción del aplicativo no es válida (mas de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")]
        public string? V_DESCRIPTION { get; set; }

        [RegularExpression(@"^(?:[^\s].*[^\s])?$", ErrorMessage = "La URL del aplicativo no es válida (mas de un espacio entre palabras, espacios al inicio o al final de la data de entrada)")]
        public string? V_URL { get; set; }

    }
}
