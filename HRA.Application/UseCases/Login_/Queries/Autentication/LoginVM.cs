using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Login_.Queries.Autentication
{
    public record LoginVM : IRequest<Iresult>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de usuario es requerido")]
        [RequiredNull(ErrorMessage = "El nombre de usuario es requerido")]
        [RegularExpression(@"^[0-9]{8,13}$", ErrorMessage = "Ingrese un usuario válido")]
        public string V_USER { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es requerida")]
        [RequiredNull(ErrorMessage = "La contraseña es requerida")]
        public string V_PASSWORD { get; set; } = string.Empty;
    }
}
