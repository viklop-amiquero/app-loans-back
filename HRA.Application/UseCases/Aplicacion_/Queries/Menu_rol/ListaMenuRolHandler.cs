using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Menu_rol
{
    public class ListaMenuRolHandler : IRequestHandler<MenuRolVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Usuario_Aplicacion> _repositoryUsuarioApp;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IRepository<Permiso> _repositoryPermiso;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ListaMenuRolHandler(
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

        public async Task<Iresult> Handle(MenuRolVM request, CancellationToken cancellationToken)
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

            // Obtencion de los menus y submenus de un rol
             var menus = _repositoryAppRolMenu.TableNoTracking.ToList()
            .Join(_repositoryMenu.TableNoTracking.ToList(), ARM => ARM.I_ID_MENU, M => M.I_ID_MENU, (ARM, M) => new { ARM, M }).Where(w => w.ARM.B_ESTADO == "1" && w.ARM.I_ID_ROL == request.I_ROLE_ID /*&& w.ARM.I_ID_PERMISO != 5*/)
            .Join(_repositoryPermiso.TableNoTracking.ToList(), ARMP => ARMP.ARM.I_ID_PERMISO, P => P.I_ID_PERMISO, (ARMP, P) => new { ARMP, P }).Where(w => w.P.B_ESTADO == "1")
            .Select(s => new Menu_
            {
                id_menu = s.ARMP.M.I_ID_MENU,
                menu = s.ARMP.M.V_MENU,
                icon = s.ARMP.M.V_ICONO,
                ruta = s.ARMP.M.V_RUTA,
                url = s.ARMP.M.V_URL,
                nivel = s.ARMP.M.I_NIVEL,
                orden = s.ARMP.M.I_ORDEN,
                id_parentesco = s.ARMP.M.V_NIVEL_PARENTESCO.ToString().ToUpper(),
                parentesco = s.ARMP.M.V_PARENTESCO,
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

            var list_menu = new List<Menu_>();

            menus.ToList().ForEach(m =>
            {
                var index_menu = 0;
                if (!string.IsNullOrEmpty(m.parentesco) && !list_menu.Exists(x => x.parentesco == m.parentesco))
                {
                    index_menu = list_menu.FindIndex(x => x.id_parentesco == m.parentesco);
                    
                } else
                {
                    index_menu = list_menu.FindLastIndex(x => x.parentesco == m.parentesco);
                }
                list_menu.Insert(index_menu + 1, m);
                list_menu.ToList();
            });

            // Lectura de menus para mostrarlo correctamente en JSON
            //var list_menu = (from x in menus
            //                 let menu = menus.FirstOrDefault(m => m.nivel == 1 && m.id_menu == x.id_menu)
            //                 let sub_menus = menus.Where(m => !string.IsNullOrEmpty(m.parentesco) && m.nivel == 2 && m.parentesco == x.id_parentesco)
            //                 where menu != null
            //                 select new Menu_
            //                 {
            //                     id_menu = menu.id_menu,
            //                     menu = menu.menu,
            //                     icon = menu.icon,
            //                     ruta = menu.ruta,
            //                     url = menu.url,
            //                     nivel = menu.nivel,
            //                     orden = menu.orden,
            //                     id_parentesco = menu.id_parentesco.ToUpper(),
            //                     parentesco = menu.parentesco,
            //                     permiso = new Permiso_DTO
            //                     {
            //                         IDPermission = menu.permiso.IDPermission,
            //                         Create = menu.permiso.Create,
            //                         Read = menu.permiso.Read,
            //                         Update = menu.permiso.Update,
            //                         Delete = menu.permiso.Delete,
            //                         Description = menu.permiso.Description
            //                     }
            //                 })
            //            //.Where(x => x.nameMenu != null
            //            //|| (x.permiso != null && x.sub_menu.Any()))
            //            .ToList();

            if (menus != null)
            {
                return new SuccessResult<List<Menu_>>(list_menu);
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
