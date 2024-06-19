using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Menu_rol
{
    public record class MenuRolVM : IRequest<Iresult>
    {
        public int I_ROLE_ID { get; set; }
    }
}
