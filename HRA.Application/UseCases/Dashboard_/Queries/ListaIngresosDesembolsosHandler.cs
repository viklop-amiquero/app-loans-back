using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Dashboard_.Queries
{
    public class ListaIngresosDesembolsosHandler : IRequestHandler<IngresosDesembolsosVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Credito> _repositoryCredito;
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IRepository<Mora> _repositoryMora;
        private readonly IRepository<Cancelacion_mora> _repositoryCancelacionMora;
        private readonly IRepository<Tipo_canc_mora> _repositoryTipoCancMora;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTime _repositoryDate;

        public ListaIngresosDesembolsosHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Credito> creditoRepository,
            IRepository<Cuota> cuotaRepository,
            IRepository<Mora> moraRepository,
            IRepository<Cancelacion_mora> cancelacionMoraRepository,
            IRepository<Tipo_canc_mora> tipoCancMoraRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IDateTime dateTime)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryCredito = creditoRepository;
            _repositoryCuota = cuotaRepository;
            _repositoryMora = moraRepository;
            _repositoryCancelacionMora = cancelacionMoraRepository;
            _repositoryTipoCancMora = tipoCancMoraRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _repositoryDate = dateTime;
        }

        public async Task<Iresult> Handle(IngresosDesembolsosVM request, CancellationToken cancellationToken)
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

            // Obtencion de los ingresos
            var ingresosDia = new List<double>();
            var ingresosMes = 0.0;

            var ingresos = _repositoryCuota.TableNoTracking.ToList()
            .Join(_repositoryMora.TableNoTracking.ToList(), C => C.I_ID_CUOTA, M => M.I_ID_CUOTA, (C, M) => new { C, M }).Where(w => w.C.B_ESTADO == "2" && w.M.B_ESTADO == "2"
             && w.C.D_FECHA_MODIFICA.Value.Year == _repositoryDate.Now.Year && w.C.D_FECHA_MODIFICA.Value.Month == _repositoryDate.Now.Month)
            .Join(_repositoryCancelacionMora.TableNoTracking.ToList(), CM => CM.M.I_ID_MORA, CA => CA.I_ID_MORA, (CM, CA) => new { CM, CA }).Where(w => w.CA.B_ESTADO == "1")
            .Join(_repositoryTipoCancMora.TableNoTracking.ToList(), CAM => CAM.CA.I_ID_TIPO_CANC_MORA, TC => TC.I_ID_TIPO_CANC_MORA, (CAM, TC) => new { CAM, TC }).Where(w => w.TC.V_NOMBRE == "Pagado")
            //.Select(w => Convert.ToDouble(w.CAM.CM.C.I_MONTO_CUOTA + w.CAM.CA.I_MONTO_CANC_MORA))
            .OrderByDescending(w => w.CAM.CM.C.D_FECHA_MODIFICA)
            .ToList();

            if (ingresos.Count() > 0)
            {
                ingresosDia = ingresos.Where(i => i.CAM.CM.C.D_FECHA_MODIFICA.Value.Day == _repositoryDate.Now.Day)
                                    .Select(i => Convert.ToDouble(i.CAM.CM.C.I_MONTO_CUOTA + i.CAM.CA.I_MONTO_CANC_MORA))
                                    .ToList();

                ingresosMes = ingresos.Select(w => Convert.ToDouble(w.CAM.CM.C.I_MONTO_CUOTA + w.CAM.CA.I_MONTO_CANC_MORA)).ToList().Sum();
            }

            // Obtencion de los desembolsos
            var creditosDia = new List<double>();
            var creditosMes = 0.0;

            var creditos = _repositoryCredito.TableNoTracking.Where(x => x.B_ESTADO == "1" && x.D_FECHA_DESEMBOLSO.Year == _repositoryDate.Now.Year && x.D_FECHA_DESEMBOLSO.Month == _repositoryDate.Now.Month)
                            //.Select(x => Convert.ToDouble(x.I_MONTO_PRESTAMO))
                            .OrderByDescending(x => x.D_FECHA_MODIFICA)
                            .ToList();

            // Obtencion de los gastos operativos
            var gastosOpDia = new List<double>();
            var gastosOpMes = 0.0;

            if (creditos.Count() > 0)
            {
                creditosDia = creditos.Where(c => c.D_FECHA_DESEMBOLSO.Day == _repositoryDate.Now.Day)
                                .Select(c => Convert.ToDouble(c.I_MONTO_PRESTAMO))
                                .ToList();

                creditosMes = creditos.Select(x => Convert.ToDouble(x.I_MONTO_PRESTAMO)).ToList().Sum();

                gastosOpDia = creditos.Where(c => c.D_FECHA_DESEMBOLSO.Day == _repositoryDate.Now.Day)
                                .Select(c => Convert.ToDouble(c.I_GASTO_FINANCIERO))
                                .ToList();

                gastosOpMes = creditos.Select(x => Convert.ToDouble(x.I_GASTO_FINANCIERO)).ToList().Sum();
            }

            // Obtencion del mayor ingreso y mayor desembolso al dia
            var maxIngresoDia = ingresosDia.Count() > 0 ? ingresosDia.Max() : 0;
            var maxDesembolsoDia = creditosDia.Count() > 0 ? creditosDia.Max() : 0;

            var ingresosDesem = new ListaIngresosDesembolsosDTO();

            ingresosDesem.V_MAX_INCOME_DAY = maxIngresoDia;
            ingresosDesem.V_MAX_DISBURSEMENT_DAY = maxDesembolsoDia;
            ingresosDesem.V_SUM_INCOMES_DAY = ingresosDia.Sum();
            ingresosDesem.V_SUM_DISBURSEMENTS_DAY = creditosDia.Sum();
            ingresosDesem.V_SUM_EXPENSES_DAY = gastosOpDia.Sum();
            ingresosDesem.V_INCOMES_DAY = ingresosDia;
            ingresosDesem.V_DISBURSEMENTS_DAY = creditosDia;
            ingresosDesem.V_EXPENSES_DAY = gastosOpDia;
            ingresosDesem.V_INCOMES_MONTH = ingresosMes;
            ingresosDesem.V_DISBURSEMENTS_MONTH = creditosMes;
            ingresosDesem.V_EXPENSES_MONTH = gastosOpMes;

            if (ingresosDesem != null)
            {
                return new SuccessResult<ListaIngresosDesembolsosDTO>(ingresosDesem);
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
