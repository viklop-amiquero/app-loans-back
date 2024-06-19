//using HRA.Application.Common.Security;
using HRA.Application.UseCases.Login_.Commands.UpdateLogin;
using HRA.Application.UseCases.Login_.Queries.Autentication;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Seguridad
{
    /// <summary>
    /// Controller authentication.
    /// </summary>
    //[Authorize(Roles = "User_app")]
    public class AuthenticationController : BaseController
    {

        /// <summary>
        /// Actualizacion de password
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /Application/Post_change_password
        ///     {
        ///         "i_USER_ID": "1",
        ///         "v_PASSWORD": "*******",
        ///         "v_NEW_PASSWORD": "***********",
        ///     }
        /// </remarks>
        /// <param name="Request"></param>
        /// <returns>Listado de usuario</returns>
        /// <response code="400">Si el elemento es nulo</response>
        /// <response code="422">Los elementos son incorrectos</response> 
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_change_password([FromBody] UpdateLoginVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Login de usuario.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Authentication/PostLogin
        ///     {
        ///         "V_USER": "28595909",
        ///         "V_PASSWORD": "**********"
        ///     }
        /// </remarks>
        /// <param name="Request"></param>
        /// <returns>Generar token de acceso</returns>
        /// <response code="400">Si el elemento es nulo</response>
        /// <response code="422">Los elementos son incorrectos</response>    
        [HttpPost]
        [AllowAnonymous]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> PostLogin([FromBody] LoginVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }
    }
}
