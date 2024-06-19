using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Rol_.DeleteRol
{
    public class DeleteRolHandler : IRequestHandler<DeleteRolVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Rol> _repositoryRol;
        private readonly IRepository<Aplicacion_Rol_Menu> _repositoryAppRolMenu;
        private readonly IRepository<Usuario_Aplicacion> _repositoryUsuarioApp;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRolHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Rol> rolRepository,
            IRepository<Aplicacion_Rol_Menu> appRolMenuRepository,
            IRepository<Usuario_Aplicacion> usuarioAppRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor,
            IMapper mapper)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryRol = rolRepository;
            _repositoryAppRolMenu = appRolMenuRepository;
            _repositoryUsuarioApp = usuarioAppRepository;
            _unitOfWork = unitOfWork;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccesor;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(DeleteRolVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryRol.Table.FirstOrDefault(x => x.I_ID_ROL == request.I_ID_ROLE && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe el rol o ya está inactivo")
                    }
                };

            }

            entity.B_ESTADO = "0";
            entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            List<int> id_users = new List<int>();
            _repositoryAppRolMenu.Table.Where(x => x.I_ID_ROL == entity.I_ID_ROL && x.B_ESTADO == "1").ToList().ForEach(a =>
            {
                a.B_ESTADO = "0";
                a.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                a.D_FECHA_MODIFICA = _repositoryDate.Now;

                _repositoryUsuarioApp.Table.Where(x => x.I_ID_APLICACION_ROL_MENU == a.I_ID_APLICACION_ROL_MENU && x.B_ESTADO == "1").ToList().ForEach(u =>
                {
                    u.B_ESTADO = "0";
                    u.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                    u.D_FECHA_MODIFICA = _repositoryDate.Now;

                    id_users.Add(u.I_ID_USUARIO);
                });
            });

            // Desactiva al usuario
            id_users = id_users.Distinct().ToList();            
            id_users.ForEach(i =>
            {
                var user = _repositoryUsuario.Table.FirstOrDefault(x => x.I_ID_USUARIO == i);
                user.B_ESTADO = "0";
                user.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                user.D_FECHA_MODIFICA = _repositoryDate.Now;
            });
            
            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}