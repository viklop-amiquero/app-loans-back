using HRA.Application.UseCases.Documento_persona_.Queries.Listado_documento_persona;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Registro
{
    public class DocumentoPersonaController : BaseController
    {
        
        /// <summary>
        /// Lista todos los documentos persona
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_documentos()
        {
            var r = await Mediator.Send(new DocumentoPersonaVM { });
            return StatusCode(r.StatusCode, r);
        }

       
    }
}
