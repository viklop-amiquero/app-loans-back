using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Persona_.Commands.ActivatePersona
{
    public class ActivatePersonaHandler : IRequestHandler<ActivatePersonaVM, Iresult>
    {
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Contacto> _repositoryContacto;
        private readonly IRepository<Contacto_emergencia> _repositoryContactoEm;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public ActivatePersonaHandler(
            IRepository<Persona> personaRepository,
            IRepository<Contacto> contactoRepository,
            IRepository<Contacto_emergencia> contactoEmRepository,
            IRepository<Documento_persona> docPersonaRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor
            )
        {
            _repositoryPersona = personaRepository;
            _repositoryContacto = contactoRepository;
            _repositoryContactoEm = contactoEmRepository;
            _repositoryDocPersona = docPersonaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(ActivatePersonaVM request, CancellationToken cancellationToken)
        {
            var claims = _httpContextAccesor?.HttpContext?.User?.Claims;
            var claimUserId = claims?.FirstOrDefault(c => c.Type == "IDUser")?.Value;

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

            var entity = _repositoryPersona.Table.FirstOrDefault(x => x.I_ID_PERSONA == request.I_PERSON_ID && x.B_ESTADO == "0");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe la persona o está activo")
                    }
                };
            }
            
            entity.B_ESTADO = "1";
            entity.I_USUARIO_MODIFICA =usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            _repositoryDocPersona.Table.Where(x => x.I_ID_PERSONA == entity.I_ID_PERSONA).ToList().ForEach(dp =>
            {
                dp.B_ESTADO = "1";
                dp.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                dp.D_FECHA_MODIFICA = _repositoryDate.Now;
            });

            _repositoryContactoEm.Table.Where(x => x.I_ID_PERSONA == entity.I_ID_PERSONA).ToList().ForEach(ce =>
            {
                ce.B_ESTADO = "1";
                ce.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                ce.D_FECHA_MODIFICA = _repositoryDate.Now;

                _repositoryContacto.Table.Where(x => x.I_ID_CONTACTO_EM == ce.I_ID_CONTACTO_EM)
                    .ToList().ForEach(c =>
                    {
                        c.B_ESTADO = "1";
                        c.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                        c.D_FECHA_MODIFICA = _repositoryDate.Now;
                    });
            });

            await _unitOfWork.CommitChanges();

            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
