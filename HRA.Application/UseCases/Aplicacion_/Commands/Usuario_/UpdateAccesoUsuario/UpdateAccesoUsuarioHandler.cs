using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Application;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.UpdateAccesoUsuario
{
    public class UpdateAccesoUsuarioHandler : IRequestHandler<UpdateAccesoUsuarioVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Clave> _repositoryClave;
        private readonly IRepository<Usuario_Aplicacion> _repositoryUserApp;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAccesoUsuarioHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Clave> claveRepository,
            IRepository<Usuario_Aplicacion> userAppRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor,
            IMapper mapper)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryClave = claveRepository;
            _repositoryUserApp = userAppRepository;
            _unitOfWork = unitOfWork;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccesor;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(UpdateAccesoUsuarioVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryUserApp.Table.Where(x => x.I_ID_USUARIO == request.I_USER_ID && x.B_ESTADO == "3" 
                                                            && x.D_FECHA_FIN < _repositoryDate.Now).ToList();

            if (entity.Count() == 0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe un acceso caducado para el usuario.")
                    }
                };
            }

            var userAcces = _repositoryUsuario.Table.FirstOrDefault(x => x.I_ID_USUARIO == request.I_USER_ID && x.B_ESTADO == "3");

            userAcces.B_ESTADO = "1";
            userAcces.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            userAcces.D_FECHA_MODIFICA = _repositoryDate.Now;

            var clave_user = _repositoryClave.Table.Where(x => x.I_ID_USUARIO == userAcces.I_ID_USUARIO && x.B_ESTADO == "3")
                                .OrderByDescending(x => x.I_ID_CLAVE).FirstOrDefault();
            clave_user.B_ESTADO = "1";
            clave_user.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            clave_user.D_FECHA_MODIFICA = _repositoryDate.Now;

            entity.ForEach(u =>
            {
                u.D_FECHA_FIN = Convert.ToDateTime(request.D_END_DATE).Date;
                u.B_ESTADO = "1";
                u.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                u.D_FECHA_MODIFICA = _repositoryDate.Now;
            });

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
