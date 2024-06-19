using HRA.Application.UseCases.Tipo_credito_.Queries.ListaTipoCredito;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class TipoCreditoController :BaseController
    {
        /// <summary>
        /// Lista todos los tipos de crédito
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_tipos_credito()
        {
            var r = await Mediator.Send(new TiposCreditosVM { });
            return StatusCode(r.StatusCode, r);
        }
    }
}
