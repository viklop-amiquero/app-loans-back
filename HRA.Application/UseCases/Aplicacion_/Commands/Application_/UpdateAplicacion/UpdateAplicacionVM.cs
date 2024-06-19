using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Application_.UpdateAplicacion
{
    public record class UpdateAplicacionVM : IRequest<Iresult>
    {
        public int I_APLICATION_ID { get; set; }

        [RequiredNull(ErrorMessage = "El nombre del aplicativo es requerido")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El nombre del aplicativo no es válido (doble espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")]
        public string V_APLICATION { get; set; } = string.Empty;

        [RegularExpression(@"^(?:[A-Za-z]+(?:[-_ ]?[A-Za-z]+)*)?$", ErrorMessage = "El acrónimo del aplicativo no es válido")]
        public string? V_ACRONYM { get; set; }

        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ0-9]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ0-9]+)*)?$", ErrorMessage = "La descripción del aplicativo no es válida (doble espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")]
        public string? V_DESCRIPTION { get; set; }

        [RegularExpression(@"^(?:[^\s].*[^\s])?$", ErrorMessage = "La URL del aplicativo no es válida (mas de un espacio entre palabras, espacios al inicio o al final de la data de entrada)")]
        public string? V_URL { get; set; }

    }
}
