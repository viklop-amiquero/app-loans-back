using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Cuenta_.Commands.NewCuenta
{
    public record class NewCuentaVM : IRequest<Iresult>
    {
        [Range(0, int.MaxValue, ErrorMessage = "El Id de persona debe ser un número entero positivo.")]
        public int I_PERSON_ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El tipo de cuenta es requerida.")]
        [RequiredNull(ErrorMessage = "El tipo de cuenta es requerida.")]
        [RegularExpression(@"^[0-9,]+(?:,[0-9,]+)*$", ErrorMessage = "El tipo decuenta no es válido (letras, caracteres especiales, espacios o debe contener solo comas y numeros ejemplo:1,2,3 ).")]
        public string V_TYPE_ACCOUNT_ID { get; set; }
    }
}
