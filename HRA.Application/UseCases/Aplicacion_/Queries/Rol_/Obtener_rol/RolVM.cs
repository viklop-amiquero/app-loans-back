using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Rol_.Obtener_rol
{
    public record class RolVM : IRequest<Iresult>
    {
        public int I_ROLE_ID { get; set; }
    }
}
