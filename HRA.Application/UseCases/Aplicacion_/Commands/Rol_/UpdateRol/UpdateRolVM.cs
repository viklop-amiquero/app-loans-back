using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Rol_.UpdateRol
{
    public record class UpdateRolVM : IRequest<Iresult>
    {
        public int I_ROLE_ID { get; set; }

        [RequiredNull(ErrorMessage = "El nombre del rol es requerido")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El nombre del rol no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_ROLE { get; set; } = string.Empty;

        [RequiredNull(ErrorMessage = "El nombre del rol es requerido")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "La descripción del rol no es válida (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_DESCRIPTION { get; set; } = string.Empty;
    }
}
