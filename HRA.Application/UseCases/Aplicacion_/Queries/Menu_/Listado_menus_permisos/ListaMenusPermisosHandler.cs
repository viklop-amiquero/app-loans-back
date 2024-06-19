using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Application.UseCases.Aplicacion_.Queries.Menu_.Obtener_menu;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Menu_.Listado_menus_permisos
{
    public class ListaMenusPermisosHandler : IRequestHandler<MenusPermisosVM, Iresult>
    {
        private readonly IRepository<Menú> _repositoryMenu;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListaMenusPermisosHandler(
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

        public async Task<Iresult> Handle(MenusPermisosVM request, CancellationToken cancellationToken)
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

            var submenus = _repositoryMenu.TableNoTracking.Where(x => x.V_PARENTESCO != null && x.B_ESTADO == "1").ToList();
            //submenus = submenus.DistinctBy(x => x.V_PARENTESCO).ToList();

            submenus.ForEach(sm =>
            {
                menus.RemoveAll(m => m.V_NIVEL_PARENTESCO.ToString().ToUpper() == sm.V_PARENTESCO);
            });

            var map = _mapper.Map<List<MenuDTO>>(menus);

            if (map != null)
            {
                return new SuccessResult<List<MenuDTO>>(map);
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
