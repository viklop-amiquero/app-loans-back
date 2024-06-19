using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Menu_access_
{
    public record class UpdateMenuAccessVM : IRequest<Iresult>
    {
        public int I_MENU_ID { get; set; }
    }
}
