using HRA.Application.UseCases.Tipo_cuenta_.Queries.ListaTipoCuenta;
using HRA.Application.UseCases.Tipo_mora_.Queries.ListaTipoMora;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Rapidiario
{
    public class TipoMoraController : BaseController
    {
        /// <summary>
        /// Lista todos los tipos de mora
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_tipos_mora()
        {
            var r = await Mediator.Send(new TiposMorasVM { });
            return StatusCode(r.StatusCode, r);
        }
    }
}
