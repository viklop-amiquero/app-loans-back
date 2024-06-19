using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Mora_.Commands.UpdateMora
{
    public record class UpdateMoraVM : IRequest<Iresult>
    {
        public int I_MORA_ID { get; set; }

        [RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        [RegularExpression(@"^$|^[0-9]+$", ErrorMessage = "La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)")]
        public string I_TYPE_CANC_MORA_ID { get; set; } = string.Empty;

        [RequiredNull(ErrorMessage = "El monto de la mora es requerido")]
        [RegularExpression(@"^([0-9]*\.?[0-9]+)?$", ErrorMessage = "El monto de la mora no es válido (letras, caracteres especiales o espacios en blanco)")]
        public string I_AMOUNT_MORA { get; set; } = string.Empty;
    }
}
