using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Menu_usuario
{
    public record class ListaMenuVM : IRequest<Iresult>
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El Id de usuario, es requerido")]
        //[RegularExpression(@"^[0-9]{1,8}$", ErrorMessage = "Ingrese un ID, valido")]
        public string V_ID_USER { get; set; } = string.Empty;
        public string V_ID_ROL { get; set; } = string.Empty;
    }
}
