using HRA.Application.UseCases.tramite_documentario_.Commands.UpdateTramiteDoc;
using HRA.Application.UseCases.Tramite_documentario_.Commands.NewTramiteDoc;
using HRA.Application.UseCases.Tramite_documentario_.Queries.ListaTramiteDoc;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class TramiteDocumentarioController : BaseController
    {

        /// <summary>
        /// Lista todos las tasas de interes
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_tramite_doc()
        {
            var r = await Mediator.Send(new TramiteDocVM { });
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Crea una tasa de interes
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_tasa_interes([FromBody] NewTramiteDocVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }


        /// <summary>
        /// Modifica un puesto
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_tasa_interes([FromBody] UpdateTramiteDocVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }
    }
}
