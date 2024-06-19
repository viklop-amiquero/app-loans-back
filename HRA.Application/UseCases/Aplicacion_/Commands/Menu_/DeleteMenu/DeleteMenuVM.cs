using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Menu_.DeleteMenu
{
    public record class DeleteMenuVM : IRequest<Iresult>
    {
        public int I_MENU_ID { get; set; }
    }
}
