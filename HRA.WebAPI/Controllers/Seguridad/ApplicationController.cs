using HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.NewAplicacionRolMenu;
using HRA.Application.UseCases.Aplicacion_.Commands.Aplicacion_rol_menu_.UpdateAplicacionRolMenu;
using HRA.Application.UseCases.Aplicacion_.Commands.Application_.NewAplicacion;
using HRA.Application.UseCases.Aplicacion_.Commands.Application_.UpdateAplicacion;
using HRA.Application.UseCases.Aplicacion_.Commands.Menu_.NewMenu;
using HRA.Application.UseCases.Aplicacion_.Commands.Menu_.UpdateMenu;
using HRA.Application.UseCases.Aplicacion_.Commands.Menu_access_;
using HRA.Application.UseCases.Aplicacion_.Commands.Usuario_Aplicacion_.NewUsuarioAplicacion;
using HRA.Application.UseCases.Aplicacion_.Commands.Usuario_Aplicacion_.UpdateUsuarioAplicacion;
using HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_menu;
using HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_rol_menu_.Listado_app_rol_menu;
using HRA.Application.UseCases.Aplicacion_.Queries.Aplicacion_rol_menu_.Obtener_app_rol_menu;
using HRA.Application.UseCases.Aplicacion_.Queries.Application.Aplicacion_menu;
using HRA.Application.UseCases.Aplicacion_.Queries.Application.Lista_total_apps;
using HRA.Application.UseCases.Aplicacion_.Queries.Application.Obtener_aplicacion;
using HRA.Application.UseCases.Aplicacion_.Queries.Menu_.Listado_menus_permisos;
using HRA.Application.UseCases.Aplicacion_.Queries.Menu_.Listado_total_menus;
using HRA.Application.UseCases.Aplicacion_.Queries.Menu_.Obtener_menu;
using HRA.Application.UseCases.Aplicacion_.Queries.Menu_usuario;
using HRA.Application.UseCases.Aplicacion_.Queries.Permiso_.Lista_total_permisos;
using HRA.Application.UseCases.Aplicacion_.Queries.Permiso_.Obtener_permiso;
using HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Listado_usuario_app;
using HRA.Application.UseCases.Aplicacion_.Queries.Usuario_Aplicacion_.Obtener_usuario_aplicacion;
using HRA.Application.UseCases.Dashboard_.Queries;
using HRA.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRA.WebAPI.Controllers.Seguridad
{
    //[Authorize(Roles = "User_app")]
    public class ApplicationController : BaseController
    {
        /// <summary>
        /// Listado de los menus de las app
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /Application/Get_lista_menu_general
        ///     {
        ///         "id": "1"
        ///     }
        /// </remarks>
        /// <param name="Request"></param>
        /// <returns>Listado de usuario</returns>
        /// <response code="400">Si el elemento es nulo</response>
        /// <response code="422">Los elementos son incorrectos</response>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_app([FromQuery] ListaAplicacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Menu aplicaion
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /Application/Get_lista_app_menu
        ///     {
        ///         "V_ID_USER": "1",
        ///         "V_ID_ROL": "1",
        ///     }
        /// </remarks>
        /// <param name="Request"></param>
        /// <returns>Listado de usuario</returns>
        /// <response code="400">Si el elemento es nulo</response>
        /// <response code="422">Los elementos son incorrectos</response> 
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_app_menu([FromQuery] ListaMenuVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        //------------------------------------APLICACION-----------------------------
        ///// <summary>
        ///// Paginado de aplicaciones
        ///// </summary>
        //[HttpGet]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Get_pagina_aplicaciones([FromQuery] ListadoAplicacionesVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        /// <summary>
        /// Lista total de aplicaciones
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_app()
        {
            var r = await Mediator.Send(new AppsVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista una aplicacion
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_aplicacion([FromQuery] AplicacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Crea una nueva aplicacion
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_aplicacion([FromBody] NewAplicacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Modifica una aplicacion
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_aplicacion([FromBody] UpdateAplicacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        ///// <summary>
        ///// Dar de baja una aplicacion
        ///// </summary>
        //[HttpPut]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Put_delete_aplicacion([FromQuery] DeleteAplicacionVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        ///// <summary>
        ///// Activa una aplicacion
        ///// </summary>
        //[HttpPatch]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Patch_activate_aplicacion([FromQuery] ActivateAplicacionVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        // -----------------------------------------MENU------------------------------------
        /// <summary>
        /// Lista de menus por aplicacion
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_menu_app([FromQuery] AplicacionMenuVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista total de menus
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_menu()
        {
            var r = await Mediator.Send(new MenusVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista de menus con permisos
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_menus_permisos()
        {
            var r = await Mediator.Send(new MenusPermisosVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista un menu
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_menu([FromQuery] MenuVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Crea un nuevo menu
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_menu([FromBody] NewMenuVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Modifica un menu
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_menu([FromBody] UpdateMenuVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Actualiza el menu en sus tablas relacionadas
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_access_menu([FromQuery] UpdateMenuAccessVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        ///// <summary>
        ///// Dar de baja un menu
        ///// </summary>
        //[HttpPut]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Put_delete_menu([FromQuery] DeleteMenuVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        ///// <summary>
        ///// Activa un menu
        ///// </summary>
        //[HttpPatch]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Patch_activate_menu([FromQuery] ActivateMenuVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        // ----------------------------------PERMISO---------------------------------
        /// <summary>
        /// Lista total de permisos
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_lista_total_permisos()
        {
            var r = await Mediator.Send(new PermisosVM { });
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista un permiso
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]

        public async Task<IActionResult> Get_permiso([FromQuery] PermisoVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        // ----------------------------------APLICACION_ROL_MENU---------------------------------
        /// <summary>
        /// Paginado de Aplicacion_rol_menu
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_pagina_app_rol_menu([FromQuery] ListadoAplicacionRolMenuVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista un registro de Aplicacion_rol_menu
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_app_rol_menu([FromQuery] AppRolMenuVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        ///<summary>
        /// Crea un registro de Aplicacion_rol_menu
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_app_rol_menu([FromBody] NewAplicacionRolMenuVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Modifica un registro de Aplicacion_rol_menu
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_app_rol_menu([FromBody] UpdateAplicacionRolMenuVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        ///// <summary>
        ///// Dar de baja un registro de Aplicacion_rol_menu
        ///// </summary>
        //[HttpPut]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Put_delete_app_rol_menu([FromQuery] DeleteAplicacionRolMenuVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        ///// <summary>
        ///// Activa un registro de Aplicacion_rol_menu
        ///// </summary>
        //[HttpPatch]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Patch_activate_app_rol_menu([FromQuery] ActivateAplicacionRolMenuVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        // ----------------------------------USUARIO_APLICACION---------------------------------
        /// <summary>
        /// Paginado de Usuario_aplicacion
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_pagina_usuario_app([FromQuery] ListadoUsuarioAplicacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Lista un registro de Usuario_aplicacion
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_user_rol_app([FromQuery] UsuarioAplicacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        ///<summary>
        /// Crea registros de Usuario_Aplicacion
        /// </summary>
        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Post_new_usuario_app([FromBody] NewUsuarioAplicacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        /// <summary>
        /// Modifica un registro de Usuario_Aplicacion
        /// </summary>
        [HttpPatch]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Patch_update_usuario_app([FromBody] UpdateUsuarioAplicacionVM Request)
        {
            var r = await Mediator.Send(Request);
            return StatusCode(r.StatusCode, r);
        }

        ///// <summary>
        ///// Dar de baja un registro de Usuario_Aplicacion
        ///// </summary>
        //[HttpPut]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Put_delete_usuario_app([FromQuery] DeleteUsuarioAplicacionVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        ///// <summary>
        ///// Activa un registro de Usuario_Aplicacion
        ///// </summary>
        //[HttpPatch]
        //[Authorize]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //[Produces("application/json")]
        //public async Task<IActionResult> Patch_activate_usuario_app([FromQuery] ActivateUsuarioAplicacionVM Request)
        //{
        //    var r = await Mediator.Send(Request);
        //    return StatusCode(r.StatusCode, r);
        //}

        // ----------------------------------DASHBOARD---------------------------------
        /// <summary>
        /// Lista de ingresos y desembolsos
        /// </summary>
        [HttpGet]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [Produces("application/json")]
        public async Task<IActionResult> Get_ingresos_desembolsos()
        {
            var r = await Mediator.Send(new IngresosDesembolsosVM { });
            return StatusCode(r.StatusCode, r);
        }
    }
}
