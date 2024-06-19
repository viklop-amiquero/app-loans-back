using HRA.Application.UseCases.Interes_ahorro_.Commands.ActivateInteresAhorro;
using HRA.Application.UseCases.Interes_ahorro_.Commands.DeleteInteresAhorro;
using HRA.Application.UseCases.Interes_ahorro_.Commands.NewInteresAhorro;
using HRA.Application.UseCases.Interes_ahorro_.Commands.UpdateInteresAhorro;
using HRA.Application.UseCases.Interes_ahorro_.Queries.Listatotal_interesahorro;
using HRA.Application.UseCases.Interes_ahorro_.Queries.ObtenerInteresAhorro;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class InteresAhorroController : BaseController
    {
         /// <summary>
        /// Lista todos los intereses de ahorro
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_interes_ahorro()
        {
            var r = await Mediator.Send(new InteresAhorroVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Obtiene un interes ahorro
        /// </summary>

        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_interes_ahorro([FromQuery] TasaAhorroVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Crea un interes de ahorro
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_interes_ahorro([FromBody] NewInteresAhorroVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Modifica un interes de ahorro
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_interes_ahorro([FromBody] UpdateInteresAhorroVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Activa un interés de ahorro
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_activate_interes_ahorro([FromQuery] ActivateInteresAhorroVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Dar de baja un interés de ahorro
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_interes_ahorro([FromQuery] DeleteInteresAhorroVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }
    }
}
