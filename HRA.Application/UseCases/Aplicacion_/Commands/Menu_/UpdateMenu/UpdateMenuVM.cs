using HRA.Application.Common.Models;
using HRA.Transversal.CustomValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Menu_.UpdateMenu
{
    public record class UpdateMenuVM : IRequest<Iresult>
    {
        public int I_MENU_ID { get; set; }

        [RequiredNull(ErrorMessage = "El menú es requerido")]
        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "El menú no es válido (doble espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")]
        public string V_NAME { get; set; } = string.Empty;

        [RegularExpression(@"^(?:[A-Za-zñÑáéíóúÁÉÍÓÚ]+(?: [A-Za-zñÑáéíóúÁÉÍÓÚ]+)*)?$", ErrorMessage = "La descripción del menú no es válida (doble espacio entre palabras, espacios al inicio o al final de la data de entrada, caracteres especiales o números)")]
        public string? V_DESCRIPTION { get; set; } = string.Empty;

        [RegularExpression(@"^(?:[^\s].*[^\s])?$", ErrorMessage = "El ícono no es válido (espacios al inicio o al final de la data de entrada)")]
        public string? V_ICON { get; set; }

        [RegularExpression(@"^(?:[^\s].*[^\s])?$", ErrorMessage = "La ruta no es válida (espacios al inicio o al final de la data de entrada)")]
        public string? V_ROUTE { get; set; }

        [RegularExpression(@"^(?:[^\s].*[^\s])?$", ErrorMessage = "La URL no es válida (espacios al inicio o al final de la data de entrada)")]
        public string? V_URL { get; set; }

        [RequiredNull(ErrorMessage = "El orden es requerido")]
        [RegularExpression(@"^$|^[0-9]+$", ErrorMessage = "El orden no es válido (letras, caracteres especiales)")]
        public string I_ORDEN { get; set; } = string.Empty;
    }
}
