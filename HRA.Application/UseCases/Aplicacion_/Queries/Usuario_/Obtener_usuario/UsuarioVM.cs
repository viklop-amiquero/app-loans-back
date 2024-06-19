using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_.Obtener_usuario
{
    public record class UsuarioVM : IRequest<Iresult>
    {
        public int I_USER_ID { get; set; }
    }
}
