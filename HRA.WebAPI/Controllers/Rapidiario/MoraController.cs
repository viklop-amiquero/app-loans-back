using HRA.Application.UseCases.Cancelacion_mora_.Queries.Lista_total_canc_mora;
using HRA.Application.UseCases.Cancelacion_mora_.Queries.ObtenerCancelacionMora;
using HRA.Application.UseCases.Mora_.Commands.UpdateMora;
using HRA.Application.UseCases.Mora_.Queries.Lista_moras_credito;
using HRA.Application.UseCases.Mora_.Queries.ObtenerMora;
using HRA.Application.UseCases.Tipo_canc_mora_.Queries.Lista_total_tipos_canc;
using HRA.Application.UseCases.Tipo_canc_mora_.Queries.Obtener_tipo_canc_mora;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class MoraController : BaseController
    {
        /// <summary>
        /// Lista total de tipos de cancelacion de mora
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_tipos_canc_mora()
        {
            var r = await Mediator.Send(new TiposCancMoraVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista un tipo de cancelacion de mora
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_usuario([FromQuery] TipoCancMoraVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista total de las cancelaciones de una mora
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_canc_mora([FromQuery] CancelacionesMoraVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista una cancelacion de una mora
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_cancelacion_mora([FromQuery] CancelacionMoraVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista de moras de un credito
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_mora_credito([FromQuery] MorasCreditoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Obtener una mora por idCuota
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]

        public async Task<IActionResult> Get_obtener_mora([FromQuery] MoraVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Modifica una mora
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_mora([FromBody] UpdateMoraVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }
    }
}
