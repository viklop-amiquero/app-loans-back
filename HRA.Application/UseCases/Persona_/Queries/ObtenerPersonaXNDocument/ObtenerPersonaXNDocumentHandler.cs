using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using NPOI.Util;

namespace HRA.Application.UseCases.Persona_.Queries.ObtenerPersonaXNDocument
{
    public class PersonaXNDocumentHandler : IRequestHandler<PersonaXNDocumentVM, Iresult>
    {
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonaXNDocumentHandler(
            IRepository<Persona> personaRepository,
            IRepository<Documento_persona> docPersonaRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryPersona = personaRepository;
            _repositoryDocPersona = docPersonaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Iresult> Handle(PersonaXNDocumentVM request, CancellationToken cancellationToken)
        {

            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
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


            /// <summary>
            ///  obtener los datos de persona por NroDocumento
            /// </summary>

            var personaData = _repositoryPersona.TableNoTracking
               .Join(_repositoryDocPersona.TableNoTracking, P => P.I_ID_PERSONA, DP => DP.I_ID_PERSONA, (P, DP) => new { DP.I_ID_PERSONA, DP.V_NRO_DOCUMENTO, DP.B_ESTADO, P.V_PRIMER_NOMBRE, P.V_SEGUNDO_NOMBRE, P.V_APELLIDO_PATERNO, P.V_APELLIDO_MATERNO })
               .Where(x => x.V_NRO_DOCUMENTO == request.V_NRO_DOCUMENT && x.B_ESTADO == "1")
               .Select(s => new PersonaXNDocumentDTO
               {

                   I_PERSON_ID = s.I_ID_PERSONA,
                   V_FIRST_NAME = s.V_PRIMER_NOMBRE,
                   V_SECOND_NAME = s.V_SEGUNDO_NOMBRE,
                   V_PATERNAL_LAST_NAME = s.V_APELLIDO_PATERNO,
                   V_MOTHER_LAST_NAME = s.V_APELLIDO_MATERNO,
                   V_NRO_DOCUMENT = request.V_NRO_DOCUMENT

               }).FirstOrDefault();

            if (personaData==null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "Persona no encontrada o esta inactivo")
                    }
                };
            }


            var map = _mapper.Map<PersonaXNDocumentDTO>(personaData);

            if (map != null)
            {
                return new SuccessResult<PersonaXNDocumentDTO>(map);
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
