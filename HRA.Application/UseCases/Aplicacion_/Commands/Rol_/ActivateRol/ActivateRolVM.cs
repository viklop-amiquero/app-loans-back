using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Rol_.ActivateRol
{
    public record class ActivateRolVM : IRequest<Iresult>
    {
        public int I_ID_ROLE { get; set; }
    }
}
