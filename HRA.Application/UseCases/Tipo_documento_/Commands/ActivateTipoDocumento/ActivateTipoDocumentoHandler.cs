using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Tipo_documento_.Commands.ActivateTipoDocumento
{
    public class ActivateTipoDocumentoHandler : IRequestHandler<ActivateTipoDocumentoVM, Iresult>
    {
        private readonly IRepository<Tipo_documento> _repositoryTipoDoc;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateTipoDocumentoHandler(
            IRepository<Tipo_documento> tipoDocRepository,
            IRepository<Usuario> repositoryUsuario,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryTipoDoc = tipoDocRepository;
            _repositoryUsuario = repositoryUsuario;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(ActivateTipoDocumentoVM request, CancellationToken cancellationToken)
        {
            var identity = _httpContextAccessor as ClaimContextAccessor;
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

            var entity = _repositoryTipoDoc.Table.FirstOrDefault(x => x.I_ID_TIPO_DOC == request.I_DOC_TYPE_ID && x.B_ESTADO == "0");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe el tipo de documento o ya está activo")
                    }
                };
            }

            entity.B_ESTADO = "1";
            entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            await _unitOfWork.CommitChanges();

            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
