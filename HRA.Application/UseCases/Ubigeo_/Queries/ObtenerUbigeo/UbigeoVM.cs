using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Ubigeo_.Queries.ObtenerUbigeo
{
    public record class UbigeoVM : IRequest<Iresult>
    {
        /// <summary>
        ///  parametro que se recibe desde el front-end
        /// </summary>
        public int I_UBIGEO_ID { get; set; }
    }
}
