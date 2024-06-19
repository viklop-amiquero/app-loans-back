using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.ActivateAplicacionRolMenu
{
    public record class ActivateAplicacionRolMenuVM : IRequest<Iresult>
    {
        public int I_APPLICATION_ROLE_MENU_ID { get; set; }
    }
}
