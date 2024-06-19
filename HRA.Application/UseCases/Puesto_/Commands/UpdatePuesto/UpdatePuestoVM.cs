using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Puesto_.Commands.UpdatePuesto
{
    public record class UpdatePuestoVM : IRequest<Iresult>
    {
        /// <summary>
        /// update tabla Puesto
        /// </summary>
        
        public int I_POSITION_ID { get; set; }

        [RequiredNull(ErrorMessage = "El nombre del puesto es requerido.")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El apellido paterno de la Puesto no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_NAME { get; set; }
       
        [RegularExpression(@"^(?:[^\s].*)?$", ErrorMessage = "La descripción del puesto no es válida (espacios al inicio o al final de la data de entrada).")]
        public string? V_DESCRIPTION { get; set; }
        
    }
}
