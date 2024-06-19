using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Application.UseCases.Aplicacion_.Queries.Menu_usuario;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Menu_.Listado_total_menus
{
    public class ListaTotalHandler : IRequestHandler<MenusVM, Iresult>
    {
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListaTotalHandler(
            IRepository<Menú> menuRepository, 
            IRepository<Usuario> usuarioRepository, 
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccessor, 
            IMapper mapper)
        {
            _repositoryMenu = menuRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(MenusVM request, CancellationToken cancellationToken)
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

            var menus = _repositoryMenu.TableNoTracking.Where(x => x.B_ESTADO == "1").OrderBy(x => x.I_NIVEL).ThenBy(x => x.I_ORDEN).ToList();

            //var submenus = _repositoryMenu.TableNoTracking.Where(x => x.V_PARENTESCO != null && x.B_ESTADO == "1").ToList();
            //submenus = submenus.DistinctBy(x => x.V_PARENTESCO).ToList();

            //submenus.ForEach(sm =>
            //{
            //    menus.RemoveAll(m => m.V_NIVEL_PARENTESCO.ToString().ToUpper() == sm.V_PARENTESCO);
            //});

            // Lectura de menus para mostrarlo correctamente en JSON
            var list_menu = (from x in menus
                             let menu = menus.FirstOrDefault(m => m.I_NIVEL == 1 && m.I_ID_MENU == x.I_ID_MENU)
                             let sub_menus = menus.Where(m => !string.IsNullOrEmpty(m.V_PARENTESCO) && m.I_NIVEL == 2 && m.V_PARENTESCO == x.V_NIVEL_PARENTESCO.ToString().ToUpper())
                             where menu != null
                             select new Menu_DTO
                             {
                                 id_menu = menu.I_ID_MENU,
                                 nameMenu = menu.V_MENU,
                                 icon = menu.V_ICONO,
                                 ruta = menu.V_RUTA,
                                 url = menu.V_URL,
                                 orden = menu.I_ORDEN,
                                 sub_menu = sub_menus.Select(m => new Sub_menu_DTO
                                 {
                                     id_menu = m.I_ID_MENU,
                                     name = m.V_MENU,
                                     icon = m.V_ICONO,
                                     ruta = m.V_RUTA,
                                     url = m.V_URL,
                                     orden = m.I_ORDEN,
                                 }).ToList()
                             })
                            //.Where(x => x.nameMenu != null
                            //|| (x.permiso != null && x.sub_menu.Any()))
                            .ToList();

            var map = _mapper.Map<List<Menu_DTO>>(list_menu);

            if (map != null)
            {
                return new SuccessResult<List<Menu_DTO>>(map);
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
