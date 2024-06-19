using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Permiso_.Obtener_permiso
{
    public record class PermisoVM : IRequest<Iresult>
    {
        public int I_PERMISSION_ID { get; set; }
    }
}
