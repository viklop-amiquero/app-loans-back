using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Rol_.NewRol
{
    public record class NewRolVM : IRequest<Iresult>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre del rol es requerido")]
        [RequiredNull(ErrorMessage = "El nombre del rol es requerido")]
        [RegularExpression(@"^[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*$", ErrorMessage = "El nombre del rol no es valido (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_ROLE { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La descripción del rol es requerida")]
        [RequiredNull(ErrorMessage = "la descripción del rol es requerida")]
        [RegularExpression(@"^[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*$", ErrorMessage = "La descripción del rol no es válida (más de un espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números).")]
        public string V_DESCRIPTION { get; set; } = string.Empty;
    }
}
