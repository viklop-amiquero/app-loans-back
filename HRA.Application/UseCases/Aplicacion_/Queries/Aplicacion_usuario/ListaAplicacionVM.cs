using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_menu
{
    public record class ListaAplicacionVM : IRequest<Iresult>
    {
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El Id de usuario, es requerido")]
        //[RegularExpression(@"^[0-9]{1,8}$", ErrorMessage = "Ingrese un ID, valido")]
        public int I_USER_ID { get; set; }
    }
}
