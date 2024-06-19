using HRA.Application.UseCases.Interes_ahorro_.Commands.ActivateInteresAhorro;
using HRA.Application.UseCases.Interes_ahorro_.Commands.DeleteInteresAhorro;
using HRA.Application.UseCases.Interes_credito_.Commands.ActivateInteresCredito;
using HRA.Application.UseCases.Interes_credito_.Commands.DeleteInteresCredito;
using HRA.Application.UseCases.Interes_credito_.Commands.NewInteresCredito;
using HRA.Application.UseCases.Interes_credito_.Commands.UpdateInteresCredito;
using HRA.Application.UseCases.Interes_credito_.Queries.Listatotal_interescredito;
using HRA.Application.UseCases.Interes_credito_.Queries.ObtenerInteresCredito;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class InteresCreditoController : BaseController
    {
        /// <summary>
        /// Lista todos los intereses de credito
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_interes_credito()
        {
            var r = await Mediator.Send(new InteresCreditoVM { });
            return StatusCode(r.StatusCode, r);
        }



        /// <summary>
        /// Obtiene un interes crédito
        /// </summary>

        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_interes_credito([FromQuery] TasaCreditoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Crea un interes de credito
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_interes_credito([FromBody] NewInteresCreditoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Modifica un interes de credito
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_interes_credito([FromBody] UpdateInteresCreditoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Activa un interés de crédito
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_activate_interes_credito([FromQuery] ActivateInteresCreditoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Dar de baja un interés de crédito
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_interes_credito([FromQuery] DeleteInteresCreditoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }
    }
}
