using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_Aplicacion_.UpdateUsuarioAplicacion
{
    public record class UpdateUsuarioAplicacionVM : IRequest<Iresult>
    {
        public int I_USER_ID { get; set; }

        [RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        [RegularExpression(@"^$|^[0-9]+$", ErrorMessage = "La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)")]
        public string I_ROLE_ID { get; set; } = string.Empty;

        [RequiredNull(ErrorMessage = "La fecha de inicio es requerida")]
        public string D_START_DATE { get; set; } = string.Empty;

        // Esto es para que, en caso de haber una fecha de fin, el usuario no pueda cambiar la fecha de fin por null con el fin de activarlo
        // Si el usuario con permisos quiere activar un registro de usuario_aplicacion, que recurra a la funcionalidad de Activar
        [RequiredNull(ErrorMessage = "La fecha de fin es requerida")]
        public string? D_END_DATE { get; set; }
    }
}
