using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Application.Aplicacion_menu
{
    public record class AplicacionMenuVM : IRequest<Iresult>
    {
        public int I_APPLICATION_ID { get; set; }
    }
}
