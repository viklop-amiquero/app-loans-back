using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Listado_usuarios_rol
{
    public record class UsuarioRolVM : IRequest<Iresult>
    {
        public int I_ROLE_ID { get; set; }
    }
}
