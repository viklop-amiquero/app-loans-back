using HRA.Application.UseCases.Tipo_cuenta_.Queries.ListaTipoCuenta;
using HRA.Application.UseCases.Tipo_cuenta_.Queries.ObtenerTipoCuenta;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class TipoCuentaController : BaseController
    {
        /// <summary>
        /// Lista el tipo de cuenta de cada usuario
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_tipo_cuenta([FromQuery] TipoCuentaVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista todos los tipos de cuenta
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_tipos_cuenta()
        {
            var r = await Mediator.Send(new TiposCuentasVM { });
            return StatusCode(r.StatusCode, r);
        }
    }
}
