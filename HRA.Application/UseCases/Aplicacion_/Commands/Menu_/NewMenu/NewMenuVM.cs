using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Menu_.NewMenu
{
    public record class NewMenuVM : IRequest<Iresult>
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "La llave foránea es requerida")]
        [RequiredNull(ErrorMessage = "La llave foránea es requerida")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "La llave foránea no es válida (letras, caracteres especiales o espacios en blanco)")]
        public string I_APPLICATION_ID { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "El menú es requerido")]
        [RequiredNull(ErrorMessage = "El menú es requerido")]
        [RegularExpression(@"^[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*$", ErrorMessage = "El menú no es válido (doble espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")]
        public string V_NAME { get; set; } = string.Empty;

        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "La descripción del menú no es válida (doble espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")]
        public string? V_DESCRIPTION { get; set; }

        [RegularExpression(@"^(?:[^\s].*[^\s])?$", ErrorMessage = "El ícono no es válido (espacios al inicio o al final de la data de entrada)")]
        public string? V_ICON { get; set; }

        [RegularExpression(@"^(?:[^\s].*[^\s])?$", ErrorMessage = "La ruta no es válida (espacios al inicio o al final de la data de entrada)")]
        public string? V_ROUTE { get; set; }

        [RegularExpression(@"^(?:[^\s].*[^\s])?$", ErrorMessage = "La URL no es válida (espacios al inicio o al final de la data de entrada)")]
        public string? V_URL { get; set; }

        [RequiredNull(ErrorMessage = "El ID del menú padre es requerido")]
        [RegularExpression(@"^$|^[0-9]+$", ErrorMessage = "El ID del menú padre no es válido (letras, caracteres especiales)")]
        // Si es submenu, ingresar el ID del menu Padre
        // Si es menu, ingresar ""
        public string? V_ID_MENU_PADRE { get; set; }
    }
}
