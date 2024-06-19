using HRA.Application.UseCases.Tipo_operacion_.Queries.ListaTipoOperacion;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class TipoOperacionController : BaseController
    {
        /// <summary>
        /// Lista todos los tipos de operación
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_tipos_operacion()
        {
            var r = await Mediator.Send(new TiposOperacionesVM { });
            return StatusCode(r.StatusCode, r);
        }
    }
}
