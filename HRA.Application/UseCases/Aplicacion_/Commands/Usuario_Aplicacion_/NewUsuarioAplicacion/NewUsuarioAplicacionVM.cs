using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_Aplicacion_.NewUsuarioAplicacion
{
    public record class NewUsuarioAplicacionVM : IRequest<Iresult>
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida")]
        //[RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)")]
        //public string I_USER_ID { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida")]
        [RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)")]
        public string I_ROLE_ID { get; set; } = string.Empty;

        //[Required(AllowEmptyStrings = false, ErrorMessage = "La fecha de inicio es requerida")]
        //[RequiredNull(ErrorMessage = "La fecha de inicio es requerida")]
        //public string D_START_DATE { get; set; } = string.Empty;
        //public string? D_END_DATE { get; set; }
    }
}
