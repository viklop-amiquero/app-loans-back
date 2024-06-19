using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Menu_usuario
{
    public class ListaMenuHandler : IRequestHandler<ListaMenuVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Usuario_Aplicacion> _repositoryUsuarioApp;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IRepository<Permiso> _repositoryPermiso;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListaMenuHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Usuario_Aplicacion> usuarioAppRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository, 
            IRepository<Menú> menuRepository,
            IRepository<Permiso> permisoRepository, 
            IMapper mapper, 
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryUsuarioApp = usuarioAppRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryMenu = menuRepository;
            _repositoryPermiso = permisoRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Iresult> Handle(ListaMenuVM request, CancellationToken cancellationToken)
        {
            var Claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var claimUserId = Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

            var usuario = _repositoryUsuario.TableNoTracking
                .Where(x => x.B_ESTADO == "1" && x.I_ID_USUARIO == Convert.ToInt32(claimUserId)).FirstOrDefault();

            if (usuario is null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("05", "Usuario no autorizado")
                    }
                };
            }

            // Obtencion de los menus y submenus a los cuales tiene acceso el usuario segun su rol
            var menus = _repositoryUsuarioApp.TableNoTracking.ToList()
            .Join(_repositoryAppRolMenu.TableNoTracking.ToList(), UA => UA.I_ID_APLICACION_ROL_MENU, ARM => ARM.I_ID_APLICACION_ROL_MENU, (UA, ARM) => new { UA, ARM }).Where(w => w.UA.B_ESTADO == "1" && w.UA.I_ID_USUARIO == Convert.ToInt32(request.V_ID_USER))
            .Join(_repositoryMenu.TableNoTracking.ToList(), UM => UM.ARM.I_ID_MENU, M => M.I_ID_MENU, (UM, M) => new { UM, M }).Where(w => w.UM.ARM.B_ESTADO == "1" && w.UM.ARM.I_ID_ROL == Convert.ToInt32(request.V_ID_ROL) && w.UM.ARM.I_ID_PERMISO != 5)
            .Join(_repositoryPermiso.TableNoTracking.ToList(), UMP => UMP.UM.ARM.I_ID_PERMISO, P => P.I_ID_PERMISO, (UMP, P) => new { UMP, P }).Where(w => w.P.B_ESTADO == "1")
            .Select(s => new Menu_
            {
                id_menu = s.UMP.M.I_ID_MENU,
                menu = s.UMP.M.V_MENU,
                icon = s.UMP.M.V_ICONO,
                ruta = s.UMP.M.V_RUTA,
                url = s.UMP.M.V_URL,
                nivel = s.UMP.M.I_NIVEL,
                orden = s.UMP.M.I_ORDEN,
                id_parentesco = s.UMP.M.V_NIVEL_PARENTESCO.ToString().ToUpper(),
                parentesco = s.UMP.M.V_PARENTESCO,
                permiso = new Permiso_DTO
                {
                    IDPermission = s.P.I_ID_PERMISO,
                    Create = s.P.I_C,
                    Read = s.P.I_R,
                    Update = s.P.I_U,
                    Delete = s.P.I_D,
                    Description = s.P.V_DESCRIPCION
                }
            }).OrderBy(o => o.nivel).ThenBy(o => o.orden).ToList();

            // Obtencion del menu padre de los submenus obtenidos anteriormente (solo para la lectura porque no esta registrado en la tabla Aplicacion_rol_menu
            menus.ToList().ForEach(m =>
            {
                                                         //!menus.Select(x => x.id_parentesco).Equals(m.parentesco)
                if (!string.IsNullOrEmpty(m.parentesco) && !menus.Exists(x => x.id_parentesco == m.parentesco))
                {
                    var menu_padre = _repositoryMenu.TableNoTracking.FirstOrDefault(x => x.V_NIVEL_PARENTESCO.ToString() == m.parentesco);
                    var menu_padre_map = new Menu_
                    {
                        id_menu = menu_padre.I_ID_MENU,
                        menu = menu_padre.V_MENU,
                        icon = menu_padre.V_ICONO,
                        ruta = menu_padre.V_RUTA,
                        url = menu_padre.V_URL,
                        nivel = menu_padre.I_NIVEL,
                        orden = menu_padre.I_ORDEN,
                        id_parentesco = menu_padre.V_NIVEL_PARENTESCO.ToString().ToUpper(),
                        parentesco = menu_padre.V_PARENTESCO,
                        permiso = new Permiso_DTO
                        {
                            IDPermission = 1,
                            Create = 1,
                            Read = 1,
                            Update = 1,
                            Delete = 1,
                            Description = ""
                        }
                    };
                    menus.Add(menu_padre_map);
                }
            });
            menus = menus.OrderBy(o => o.nivel).ThenBy(o => o.orden).ToList();

            // Lectura de menus para mostrarlo correctamente en JSON
            var list_menu = (from x in menus
                             let menu = menus.FirstOrDefault(m => m.nivel == 1 && m.id_menu == x.id_menu)
                             let sub_menus = menus.Where(m => !string.IsNullOrEmpty(m.parentesco) && m.nivel == 2 && m.parentesco == x.id_parentesco)
                             where menu != null
                             select new Menu_DTO
                             {
                                 id_menu = menu.id_menu,
                                 nameMenu = menu.menu,
                                 icon = menu.icon,
                                 ruta = menu.ruta,
                                 url = menu.url,
                                 orden = menu.orden,
                                 permiso = menu.permiso,
                                 sub_menu = sub_menus.Select(m => new Sub_menu_DTO
                                 {
                                     id_menu = m.id_menu,
                                     name = m.menu,
                                     icon = m.icon,
                                     ruta = m.ruta,
                                     url = m.url,
                                     orden = m.orden,
                                     permiso = m.permiso
                                 }).ToList()
                             })
                        //.Where(x => x.nameMenu != null
                        //|| (x.permiso != null && x.sub_menu.Any()))
                        .ToList();

            //var x = menus.Find(x => x.id_menu == 4);
            //var sub_menus = menus.Where(m => !string.IsNullOrEmpty(m.parentesco) && m.nivel == 2 && m.parentesco == x.id_parentesco.ToUpper());

            if (menus != null)
            {
                return new SuccessResult<List<Menu_DTO>>(list_menu);
            }
            else
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("01", "No se pudo obtener respuesta.")
                    }
                };
            }
        }
    }
}
