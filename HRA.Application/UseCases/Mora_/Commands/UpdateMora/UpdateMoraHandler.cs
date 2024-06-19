using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Mora_.Commands.UpdateMora
{
    public class UpdateMoraHandler : IRequestHandler<UpdateMoraVM, Iresult>
    {
        private readonly IRepository<Mora> _repositoryMora;
        private readonly IRepository<Tipo_mora> _repositoryTipoMora;
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IRepository<Cancelacion_mora> _repositoryCancMora;
        private readonly IRepository<Tipo_canc_mora> _repositoryTipoCancMora;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateMoraHandler(
            IRepository<Mora> moraRepository,
            IRepository<Tipo_mora> tipoMoraRepository,
            IRepository<Cuota> cuotaRepository,
            IRepository<Cancelacion_mora> cancMoraRepository,
            IRepository<Tipo_canc_mora> tipoCancMoraRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryMora = moraRepository;
            _repositoryTipoMora = tipoMoraRepository;
            _repositoryCuota = cuotaRepository;
            _repositoryCancMora = cancMoraRepository;
            _repositoryTipoCancMora = tipoCancMoraRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateMoraVM request, CancellationToken cancellationToken)
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

            var entity = _repositoryMora.Table.FirstOrDefault(x => x.I_ID_MORA == request.I_MORA_ID && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe la mora o está inactiva o ya ha sido pagada")
                    }
                };
            }

            var cuota = _repositoryCuota.Table.FirstOrDefault(x => x.I_ID_CUOTA == entity.I_ID_CUOTA && x.B_ESTADO == "1");

            if (cuota == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe la cuota vinculada a la mora o está inactiva o ya ha sido pagada")
                    }
                };
            }

            var tipo_mora = _repositoryTipoMora.TableNoTracking.FirstOrDefault(x => x.I_ID_TIPO_MORA == entity.I_ID_TIPO_MORA && x.B_ESTADO == "1");

            if (tipo_mora == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el tipo de la mora o está inactivo")
                    }
                };
            }

            var tipo_canc_mora = _repositoryTipoCancMora.TableNoTracking.FirstOrDefault(x => x.I_ID_TIPO_CANC_MORA.ToString() == request.I_TYPE_CANC_MORA_ID && x.B_ESTADO == "1");

            if (tipo_canc_mora == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "No existe el tipo de cancelacion de la mora o está inactivo")
                    }
                };
            }

            var monto_mora = Convert.ToDecimal(request.I_AMOUNT_MORA);

            if (monto_mora > 0 && monto_mora <= entity.I_MONTO_MORA)
            {
                var canc_mora = new Cancelacion_mora
                {
                    I_ID_MORA = entity.I_ID_MORA,
                    I_ID_TIPO_CANC_MORA = tipo_canc_mora.I_ID_TIPO_CANC_MORA,
                    I_MONTO_CANC_MORA = monto_mora,
                    I_MONTO_INICIAL_MORA = entity.I_MONTO_MORA,
                    I_MONTO_FINAL_MORA = entity.I_MONTO_MORA - monto_mora,
                    B_ESTADO = "1",
                    I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                    D_FECHA_CREACION = _repositoryDate.Now
                };
                _repositoryCancMora.Insert(canc_mora);

                entity.I_MONTO_MORA = canc_mora.I_MONTO_FINAL_MORA;
                entity.B_ESTADO = canc_mora.I_MONTO_FINAL_MORA == 0 ? "2" : "1"; //Estado "2" para pagado y "1" pendiente de pago
                entity.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                entity.D_FECHA_MODIFICA = _repositoryDate.Now;

                cuota.I_MONTO_TOTAL = cuota.I_MONTO_CUOTA + entity.I_MONTO_MORA;
                cuota.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                cuota.D_FECHA_MODIFICA = _repositoryDate.Now;

                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);
            }

            return new FailureResult<IEnumerable<DetailError>>()
            {
                StatusCode = 400,
                Value = new List<DetailError>()
                {
                    new DetailError("02", "El monto ingresado excede sus límites aceptados")
                }
            };
        }
    }
}
