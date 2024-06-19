using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Operaciones;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Tipo_documento_.Commands.DeleteTipoDocumento
{
    public class DeleteTipoDocumentoHandler : IRequestHandler<DeleteTipoDocumentoVM, Iresult>
    {
        private readonly IRepository<Tipo_documento> _repositoryTipoDocumento;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTipoDocumentoHandler(
            IRepository<Tipo_documento> tipoDocumentoRepository,
            IRepository<Documento_persona> docPersRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccesor)
        {
            _repositoryTipoDocumento = tipoDocumentoRepository;
            _repositoryDocPersona = docPersRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(DeleteTipoDocumentoVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryTipoDocumento.Table.FirstOrDefault(x => x.I_ID_TIPO_DOC== request.I_DOC_TYPE_ID);
            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe el tipo de documento")
                    }
                };
            }

            entity.B_ESTADO = "0";
            entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            _repositoryDocPersona.Table.Where(x => x.I_ID_TIPO_DOC == entity.I_ID_TIPO_DOC).ToList().ForEach(d =>
            {
                d.B_ESTADO = "0";
                d.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                d.D_FECHA_MODIFICA = _repositoryDate.Now;
            });

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
