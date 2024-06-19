using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Operacion_.Commands.NewOperacion
{
    public record class NewOperacionVM : IRequest<Iresult>
    {
        public string V_ID_ACCOUNT { get; set; }
        public string? V_ID_CUOTA { get; set; }
        public string V_ID_TYPE_OPERATION { get; set; }
        public string V_AMOUNT { get; set; }
    }
}
