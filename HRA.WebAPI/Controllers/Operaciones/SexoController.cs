using HRA.Application.UseCases.Sexo_.Queries.Lista_total_sexos;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Operaciones
{
    public class SexoController : BaseController
    {

        /// <summary>
        /// Lista todos los sexos
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_sexo()
        {
            var r = await Mediator.Send(new SexosVM { });
            return StatusCode(r.StatusCode, r);
        }

    }
}
