using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Queries.Usuario_.Obtener_pers_usuario
{
    public class ObtenerUsuarioPersHandler : IRequestHandler<UsuarioPersVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ObtenerUsuarioPersHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Persona> personaRepository,
            IRepository<Documento_persona> docPersonaRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryPersona = personaRepository;
            _repositoryDocPersona = docPersonaRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(UsuarioPersVM request, CancellationToken cancellationToken)
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

            var doc = _repositoryDocPersona.TableNoTracking.Where(x => x.V_NRO_DOCUMENTO == request.V_USER_NAME && x.B_ESTADO == "1").ToList();

            if (doc.Count() == 0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "La persona no existe o está inactiva")
                    }
                };
            }

            var persona = _repositoryPersona.TableNoTracking.Where(x => x.I_ID_PERSONA == doc.First().I_ID_PERSONA).ToList();

            var map = _mapper.Map<List<UsuarioPersDTO>>(persona);

            var user_pers = _repositoryUsuario.TableNoTracking.FirstOrDefault(x => x.I_ID_PERSONA == persona.First().I_ID_PERSONA
                                                                            && x.B_ESTADO == "1");

            if (map != null)
            {
                map.ForEach(u =>
                {
                    u.I_USER_ID = user_pers == null ? 0 : user_pers.I_ID_USUARIO;
                    u.V_USER = user_pers == null ? "" : user_pers.V_USUARIO;
                });

                return new SuccessResult<List<UsuarioPersDTO>>(map);
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
