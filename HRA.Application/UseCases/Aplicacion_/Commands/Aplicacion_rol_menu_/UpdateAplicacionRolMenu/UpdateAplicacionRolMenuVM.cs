using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.UpdateAplicacionRolMenu
{
    public record class UpdateAplicacionRolMenuVM : IRequest<Iresult>
    {
        //public int I_ROLE_ID { get; set; }
        public int I_APPLICATION_ROLE_MENU_ID { get; set; }

        // NO TIENE SENTIDO EDITAR EL MENU O ROL
        //[RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        //[RegularExpression(@"^$|^[0-9]+$", ErrorMessage = "La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)")]
        //public string I_ROLE_ID { get; set; }

        //[RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        //[RegularExpression(@"^$|^[0-9]+$", ErrorMessage = "La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)")]
        //public string I_MENU_ID { get; set; }

        [RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        public string I_PERMISSION_ID { get; set; } = string.Empty;
    }
}
