using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Login_.Commands.UpdateLogin
{
    public record UpdateLoginVM : IRequest<Iresult>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "El ID es requerido")]
        [RequiredNull(ErrorMessage = "El ID es requerido")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese un ID valido")]
        public string V_ID_USER { get; set; } = string.Empty;

        //[Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de usuario es requerido")]
        //[RequiredNull(ErrorMessage = "El nombre de usuario es requerido")]
        //[RegularExpression(@"^[0-9]{8,13}$", ErrorMessage = "Ingrese un usuario valido (solo se aceptan numeros)")]
        //public string V_USUARIO { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es requerida")]
        [RequiredNull(ErrorMessage = "La contraseña es requerida")]
        //[StringLength(80, MinimumLength = 8, ErrorMessage = "La contraseña no es correcta")]
        public string V_PASSWORD { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La nueva contraseña es requerida")]
        [RequiredNull(ErrorMessage = "La nueva contraseña es requerida")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*]).{8,16}$", ErrorMessage = "Ingrese una contraseña que contenga como mínimo una letra mayúscula, una letra minúscula, un dígito y un carácter especial")]
        //[Compare(nameof(V_CONFIRM_PASSWORD), ErrorMessage = "No coincide los password")]
        public string V_NEW_PASSWORD { get; set; } = string.Empty;

        //[Required(AllowEmptyStrings = false, ErrorMessage = "El password, es requerido")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[!@#$%^&*])(?=.*[0-9]).{8,13}$", ErrorMessage = "Ingrese un password que contenga como mínimo una mayuscula, número y un simbolo.")]
        //[Compare(nameof(V_NEW_PASSWORD), ErrorMessage = "No coincide los password")]
        //public string V_CONFIRM_PASSWORD { get; set; }
    }
}
