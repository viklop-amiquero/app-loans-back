using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Interes_ahorro_.Commands.UpdateInteresAhorro
{
    public record class UpdateInteresAhorroVM : IRequest<Iresult>
    {

        /// <summary>
        /// Update tabla interes_ahorro
        /// </summary>    
        public int I_INTEREST_ID { get; set; }

        [RequiredNull(ErrorMessage = "La tasa de interés es requerido.")]
        [RegularExpression(@"^(?!.*\s)\d*(\.\d{1,2})?$", ErrorMessage = "La tasa de interés no es valido (espacios al inicio o al final, solo debe tener dos decimales, debe ser solo numero).")]
        public string I_INTEREST { get; set; }

        [RequiredNull(ErrorMessage = "El nombre de la tasa de interes es requerido.")]
        [RegularExpression(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$", ErrorMessage = "El nombre de la tasa de interes no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_NAME { get; set; }

        [RegularExpression(@"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ]*$", ErrorMessage = "La frecuencia no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string? V_FREQUENCY { get; set; }

        [RegularExpression(@"^(?:[^\s].*)?$", ErrorMessage = "La descripción de la tasa de interes  no es válido (espacios al inicio o al final de la data de entrada).")]
        public string? V_DESCRIPTION { get; set; }
    }
}
