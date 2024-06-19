using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Tramite_documentario_.Commands.NewTramiteDoc
{
    public record class NewTramiteDocVM : IRequest<Iresult>
    {
        /// <summary>
        /// Insert tabla tasa_interes
        /// </summary> 
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del tramite documentario es requerido.")]
        [RequiredNull(ErrorMessage = "El nombre del tramite documentario es requerido.")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El nombre del tramite documentario no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_NAME { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La tasa de interés es requerido.")]
        [RequiredNull(ErrorMessage = "La tasa de interés es requerido.")]
        [RegularExpression(@"^(?!.*\s)\d+(\.\d{1,2})?$", ErrorMessage = "La tarifa del tramite documentario no es valido (espacios al inicio o al final, solo debe tener dos decimales, debe ser solo numero).")]
        public string I_FEE { get; set; }
        public string? V_DESCRIPTION { get; set; }

    }
}
