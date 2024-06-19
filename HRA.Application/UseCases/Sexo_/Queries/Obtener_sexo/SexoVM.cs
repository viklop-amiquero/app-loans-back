using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Sexo_.Queries.Obtener_sexo
{
    public record class SexoVM : IRequest<Iresult>
    {
        public string? I_SEX_ID { get; set; }
    }
}
