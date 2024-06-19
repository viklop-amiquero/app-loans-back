using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Interes_ahorro_.Queries.ObtenerInteresAhorro
{
    public record class TasaAhorroVM : IRequest<Iresult>
    {
        /// <summary>
        ///  parametro que se recibe desde el front-end
        /// </summary>
        public int I_INTEREST_ID { get; set; }
    }
}
