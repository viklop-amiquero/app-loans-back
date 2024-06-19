using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Rol_.DeleteRol
{
    public record class DeleteRolVM : IRequest<Iresult>
    {
        public int I_ID_ROLE { get; set; }
    }
}
