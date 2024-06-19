using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.tramite_documentario_.Commands.UpdateTramiteDoc
{
    public class UpdateTramiteDocumentarioHandler : IRequestHandler<UpdateTramiteDocVM, Iresult>
    {
        private readonly IRepository<Tramite_documentario> _repositoryTramiteDoc;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTramiteDocumentarioHandler(
            IRepository<Tramite_documentario> tramiteDocRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryTramiteDoc = tramiteDocRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateTramiteDocVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryTramiteDoc.Table.FirstOrDefault(x => x.I_ID_TRAMITE_DOC == int.Parse( request.V_PROCEDURE_DOC_ID) && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe el trámite documentario o está inactivo")
                    }
                };

            }
            request.V_NAME = request.V_NAME.ToUpper();

            if (_repositoryTramiteDoc.TableNoTracking.Where(x => x.V_NOMBRE == request.V_NAME && x.I_ID_TRAMITE_DOC != Convert.ToInt32(request.V_PROCEDURE_DOC_ID)).ToList().Count == 0)
            {
                entity.I_ID_TRAMITE_DOC = request.V_PROCEDURE_DOC_ID == "" ? entity.I_ID_TRAMITE_DOC : int.Parse(request.V_PROCEDURE_DOC_ID);
                entity.V_NOMBRE = request.V_NAME == "" ? entity.V_NOMBRE : request.V_NAME;
                entity.I_TARIFA = request.V_FEE == "" ? entity.I_TARIFA : decimal.Parse(request.V_FEE);
                entity.V_DESCRIPCION = request.V_DESCRIPTION == "" ? entity.V_DESCRIPCION : request.V_DESCRIPTION;
                entity.B_ESTADO = "1";
                entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                entity.D_FECHA_MODIFICA = _repositoryDate.Now;

                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);

            }
            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                    new DetailError("06", "Registro ya existente")
                }
            };
        }
    }
}
