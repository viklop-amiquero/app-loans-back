using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.NewUsuario
{
    public record class NewUsuarioVM : IRequest<Iresult>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida.")]
        [RequiredNull(ErrorMessage = "La llave foránea es requerida.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "El ID de la llave foránea no es válido (letras, caracteres especiales o espacios en blanco)")]
        public string I_PERSON_ID { get; set; } = string.Empty;

        //[Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de usuario es requerido")]
        //[RequiredNull(ErrorMessage = "El nombre de usuario es requerido")]
        //[RegularExpression(@"^[0-9]{8,13}$", ErrorMessage = "Ingrese un usuario válido (solo se aceptan números)")]
        //public string V_NAME { get; set; } = string.Empty;

        //[Required(AllowEmptyStrings = false, ErrorMessage = "La contraseña es requerida")]
        //[RequiredNull(ErrorMessage = "La contraseña es requerida")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*]).{8,16}$", ErrorMessage = "Ingrese una contraseña que contenga como mínimo una letra mayúscula, una letra minúscula, un dígito y un carácter especial")]
        //public string V_PASS { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida")]
        [RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)")]
        public string I_ROLE_ID { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha de inicio es requerida")]
        [RequiredNull(ErrorMessage = "La fecha de inicio es requerida")]
        public string D_START_DATE { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La fecha de fin es requerida")]
        [RequiredNull(ErrorMessage = "La fecha de fin es requerida")]
        public string D_END_DATE { get; set; } = string.Empty;
    }
}
