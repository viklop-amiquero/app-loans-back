using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Menu_.Obtener_menu
{
    public record class MenuVM : IRequest<Iresult>
    {
        public int I_MENU_ID { get; set; }
    }
}
