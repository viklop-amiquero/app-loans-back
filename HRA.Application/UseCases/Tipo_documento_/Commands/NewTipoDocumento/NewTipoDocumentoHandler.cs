using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Tipo_documento_.Commands.NewTipoDocumento
{
    public class NewTipoDocumentoHandler : IRequestHandler<NewTipoDocumentoVM, Iresult>
    {
        private readonly IRepository<Tipo_documento> _repositoryTipoDocumento;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public NewTipoDocumentoHandler(
            IRepository<Tipo_documento> tipoDocumentoRepository, 
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccesor)
        {
            _repositoryTipoDocumento = tipoDocumentoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(NewTipoDocumentoVM request, CancellationToken cancellationToken)
        {
            var identity = _httpContextAccesor as ClaimContextAccessor;
            var usuario = _repositoryUsuario.Table.FirstOrDefault(x => x.V_USUARIO == Convert.ToString(identity!.UserName));

            if (usuario == null)
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

            request.V_DOC_NAME = request.V_DOC_NAME.ToUpper();
            if (_repositoryTipoDocumento.TableNoTracking.Where(
                x => x.V_NOMBRE_DOC == request.V_DOC_NAME || x.V_ABREVIATURA == request.V_ABBREVIATION).ToList().Count == 0)
            {
                _repositoryTipoDocumento.Insert(new List<Tipo_documento>
                {
                    new Tipo_documento
                    {
                        V_ABREVIATURA = request.V_ABBREVIATION == "" ? null : (request.V_ABBREVIATION == "null" ? null : request.V_ABBREVIATION?.ToUpper()),
                        V_NOMBRE_DOC = request.V_DOC_NAME,
                        I_NRO_DIGITOS = request.I_DIGITS_NUMBER == "" ? null : (request.I_DIGITS_NUMBER == "null" ? null : Convert.ToInt32(request.I_DIGITS_NUMBER)),
                        B_ESTADO = "1",
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                        D_FECHA_CREACION = _repositoryDate.Now,
                    }
                });

                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);
            }

            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                    new DetailError("06", "Registro ya existente (nombre o abreviatura) o registro inactivo")
                }
            };
        }
    }
}
