using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.NewAplicacionRolMenu
{
    public record class NewAplicacionRolMenuVM : IRequest<Iresult>
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida")]
        //[RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        //public string I_APP_ID { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida")]
        [RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        public string I_ROLE_ID { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida")]
        [RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        public string I_MENU_ID { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida")]
        [RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        public string I_PERMISSION_ID { get; set; } = string.Empty;
    }
}
