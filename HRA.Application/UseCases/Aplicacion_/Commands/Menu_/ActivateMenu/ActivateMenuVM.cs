using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Menu_.ActivateMenu
{
    public record class ActivateMenuVM : IRequest<Iresult>
    {
        public int I_MENU_ID { get; set; }
    }
}
