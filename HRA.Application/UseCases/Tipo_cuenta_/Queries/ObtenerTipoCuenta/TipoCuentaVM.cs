using HRA.Application.Common.Models;
using MediatR;

namespace HRA.Application.UseCases.Tipo_cuenta_.Queries.ObtenerTipoCuenta
{
    public record class TipoCuentaVM: IRequest<Iresult>
    {
        /// <summary>
        ///  parametro que se recibe desde el front-end
        /// </summary>
        public int I_PERSON_ID { get; set; }
    }
}
