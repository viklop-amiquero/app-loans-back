using HRA.Application.UseCases.Tipo_documento_.Commands.ActivateTipoDocumento;
using HRA.Application.UseCases.Tipo_documento_.Commands.DeleteTipoDocumento;
using HRA.Application.UseCases.Tipo_documento_.Commands.NewTipoDocumento;
using HRA.Application.UseCases.Tipo_documento_.Commands.UpdateTipoDocumento;
using HRA.Application.UseCases.Tipo_documento_.Queries.Lista_total_tipos_documento;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Operaciones
{
    public class TipoDocumentoController : BaseController
    {
        
        /// <summary>
        /// Lista todos los tipos de documento
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_tipodocumentos()
        {
            var r = await Mediator.Send(new TiposDocumentoVM { });
            return StatusCode(r.StatusCode, r);
        }
        /// <summary>
        /// Crea un nuevo tipo de documento
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_tipo_doc([FromBody] NewTipoDocumentoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Modifica un tipo de documento
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_tipo_doc([FromBody] UpdateTipoDocumentoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Activa un tipo de documento
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_activate_tipo_doc([FromQuery] ActivateTipoDocumentoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Dar de baja un tipo de documento
        /// </summary>
        [HttpPut]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Put_delete_tipo_doc([FromQuery] DeleteTipoDocumentoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }
    }
}
