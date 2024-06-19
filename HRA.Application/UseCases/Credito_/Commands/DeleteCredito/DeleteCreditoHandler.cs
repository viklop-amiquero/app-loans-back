using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Credito_.Commands.DeleteCredito
{
    public class DeleteCreditoHandler : IRequestHandler<DeleteCreditoVM, Iresult>
    {
        private readonly IRepository<Credito> _repositoryCredito;
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCreditoHandler(
            IRepository<Credito> creditoRepository, 
            IRepository<Cuota> cuotaRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccesor)
        {
            _repositoryCredito= creditoRepository;
            _repositoryCuota= cuotaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(DeleteCreditoVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryCredito.Table.FirstOrDefault(x => x.I_ID_CREDITO == request.I_CREDITO_ID);

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe el crédito")
                    }
                };

            }

            entity.B_ESTADO = "0";
            entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            _repositoryCuota.Table.Where(x => x.I_ID_CREDITO == entity.I_ID_CREDITO).ToList().ForEach(p =>
            {
                p.B_ESTADO = "0";
                p.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                p.D_FECHA_MODIFICA = _repositoryDate.Now;
            });

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}
