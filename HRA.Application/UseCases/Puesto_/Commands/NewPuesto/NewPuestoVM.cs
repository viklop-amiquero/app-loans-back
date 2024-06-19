using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Puesto_.Commands.NewPuesto
{
    public record class NewPuestoVM : IRequest<Iresult>
    {
        /// <summary>
        /// Insert tabla puesto
        /// </summary>        
 
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del puesto es requerido.")]
        [RequiredNull(ErrorMessage = "El nombre del puesto es requerido.")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El nombre delpuesto no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_NAME { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripcion del puesto es requerido.")]
        [RegularExpression(@"^[^\s].*[^\s]$", ErrorMessage = "La descripcion del puesto no es válida (espacios al inicio o al final de la data de entrada).")]
        public string? V_DESCRIPTION { get; set; }
    }
}
