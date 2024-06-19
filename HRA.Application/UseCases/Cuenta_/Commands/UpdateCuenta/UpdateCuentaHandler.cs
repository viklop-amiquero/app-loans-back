using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using HRA.Transversal.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Cuenta_.Commands.UpdateCuenta
{
    public class UpdateCuentaHandler : IRequestHandler<UpdateCuentaVM, Iresult>
    {
        private readonly IRepository<Cuenta> _repositoryCuenta;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCuentaHandler(
            IRepository<Cuenta> cuentaRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryCuenta = cuentaRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateCuentaVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryCuenta.Table.FirstOrDefault(x => x.I_ID_CUENTA == request.I_ID_ACCOUNT && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe la cuenta o está inactivo")
                    }
                };

            }

            entity.I_ID_TIPO_CUENTA= request.V_ID_ACCOUNT_TYPE == "" ? entity.I_ID_TIPO_CUENTA : int.Parse(request.V_ID_ACCOUNT_TYPE);
            entity.I_SALDO = request.V_BALANCE == "" ? entity.I_SALDO : decimal.Parse(request.V_BALANCE);
            entity.V_NUMERO_CUENTA = request.V_ACCOUNT_NUMBER == "" ? entity.V_NUMERO_CUENTA : request.V_ACCOUNT_NUMBER;
           
            entity.B_ESTADO = "1";
            entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);



            //return new FailureResult<IEnumerable<DetailError>>()
            //{
            //    StatusCode = 400,
            //    Value = new List<DetailError>()
            //    {
            //        new DetailError("06", "Credito o cuenta inexistente")
            //    }
            //};
        }
    }
}
