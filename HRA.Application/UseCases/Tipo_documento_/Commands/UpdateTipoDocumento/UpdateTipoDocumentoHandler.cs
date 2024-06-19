using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Tipo_documento_.Commands.UpdateTipoDocumento
{
    public class UpdateTipoDocumentoHandler : IRequestHandler<UpdateTipoDocumentoVM, Iresult>
    {
        private readonly IRepository<Tipo_documento> _repositoryTipoDocumento;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTipoDocumentoHandler(
            IRepository<Tipo_documento> tipoDocumentoRepository, 
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryTipoDocumento = tipoDocumentoRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateTipoDocumentoVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryTipoDocumento.Table.FirstOrDefault(x => x.I_ID_TIPO_DOC == request.I_DOC_TYPE_ID && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el tipo de documento o está inactivo")
                    }
                };
            }

            request.V_DOC_NAME = request.V_DOC_NAME.ToUpper();
            if (_repositoryTipoDocumento.TableNoTracking.Where(x =>
                    (x.V_NOMBRE_DOC == request.V_DOC_NAME || x.V_ABREVIATURA == request.V_ABBREVIATION)
                    && x.I_ID_TIPO_DOC != request.I_DOC_TYPE_ID).ToList().Count == 1)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 400,
                    Value = new List<DetailError>()
                    {
                        new DetailError("06", "Registro ya existente (nombre o abreviatura)")
                    }
                };
            }

            entity.V_ABREVIATURA = request.V_ABBREVIATION == "" ? entity.V_ABREVIATURA : (request.V_ABBREVIATION == "null" ? null : request.V_ABBREVIATION?.ToUpper());
            entity.V_NOMBRE_DOC = request.V_DOC_NAME == "" ? entity.V_NOMBRE_DOC : request.V_DOC_NAME;
            entity.I_NRO_DIGITOS = request.I_DIGITS_NUMBER == "" ? entity.I_NRO_DIGITOS : (request.I_DIGITS_NUMBER == "null" ? null : Convert.ToInt32(request.I_DIGITS_NUMBER));
            entity.B_ESTADO = "1";
            entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
