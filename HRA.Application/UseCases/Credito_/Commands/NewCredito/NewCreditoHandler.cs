using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Registro;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Credito_.Commands.NewCredito
{
    public class NewCreditoHandler : IRequestHandler<NewCreditoVM, Iresult>
    {
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Documento_persona> _repositoryDocPersona;
        private readonly IRepository<Puesto_persona> _repositoryPuestoPers;
        private readonly IRepository<Credito> _repositoryCredito;
        private readonly IRepository<Cuenta> _repositoryCuenta;
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IRepository<Interes_credito> _repositoryInteresCredito;
        private readonly IRepository<Tramite_documentario> _repositoryTramiteDoc;
        private readonly IRepository<Tramite_cuenta> _repositoryTramiteCuenta;
        private readonly IRepository<Operacion> _repositoryOperacion;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public NewCreditoHandler(
            IRepository<Persona> personaRepository,
            IRepository<Documento_persona> docPersonaRepository,
            IRepository<Puesto_persona> puestoPersRepository,
            IRepository<Credito> creditoRepository,
            IRepository<Cuenta> cuentaRepository,
            IRepository<Cuota> cuotaRepository,
            IRepository<Interes_credito> interesCreditoRepository,
            IRepository<Tramite_documentario> tramiteDocRepository,
            IRepository<Tramite_cuenta> tramiteCuentaRepository,
            IRepository<Operacion> operacionRepository,
            IRepository<Usuario> usuarioRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor)
        {
            _repositoryPersona = personaRepository;
            _repositoryDocPersona = docPersonaRepository;
            _repositoryPuestoPers = puestoPersRepository;
            _repositoryCredito = creditoRepository;
            _repositoryCuenta = cuentaRepository;
            _repositoryCuota = cuotaRepository;
            _repositoryInteresCredito = interesCreditoRepository;
            _repositoryTramiteDoc = tramiteDocRepository;
            _repositoryTramiteCuenta = tramiteCuentaRepository;
            _repositoryOperacion = operacionRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(NewCreditoVM request, CancellationToken cancellationToken)
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
            var persona = _repositoryDocPersona.TableNoTracking.Where(x => x.I_ID_PERSONA == request.I_PERSON_ID && x.B_ESTADO == "1").FirstOrDefault();
            if (persona is null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 400,
                    Value = new List<DetailError>()
                    {
                        new DetailError("06","Cliente inactivo")
                    }
                };
            }
            decimal interesCredito = _repositoryInteresCredito.Table.FirstOrDefault(x => x.I_ID_INTERES_CREDITO == request.I_INTEREST_CREDIT_ID && x.B_ESTADO == "1")!.I_TASA_INTERES;

            var dni = _repositoryDocPersona.TableNoTracking.Where(x => x.I_ID_PERSONA == request.I_PERSON_ID && x.B_ESTADO == "1").FirstOrDefault()!.V_NRO_DOCUMENTO;
            //Creando numero de cuenta de 10 digitos
            int anio = DateTime.Now.Year % 100;
            string numeroCuenta = "";

            if (_repositoryCuenta.TableNoTracking.Where(x => x.I_ID_PERSONA == request.I_PERSON_ID && x.I_ID_TIPO_CUENTA == 2).ToList().Count == 0)
            {
                numeroCuenta = "02" + dni + anio;
                if (_repositoryCuenta.TableNoTracking.Where(x => x.V_NUMERO_CUENTA.Substring(0, 10) == numeroCuenta.Substring(0, 10)).ToList().Count == 0)
                {
                    /// <summary>                             
                    /// insertar tabla Cuenta
                    /// </summary>                     

                    var newCuenta = new Cuenta
                    {
                        I_ID_PERSONA = request.I_PERSON_ID,
                        I_ID_TIPO_CUENTA = 2,
                        V_NUMERO_CUENTA = numeroCuenta,
                        I_SALDO = decimal.Parse(request.V_LOAN_AMOUNT),
                        B_ESTADO = "1",
                        D_FECHA_CREACION = _repositoryDate.Now,
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO
                    };
                    _repositoryCuenta.Insert(new List<Cuenta> { newCuenta });
                }
                /// <summary>
                /// insertar tabla puesto_persona
                /// </summary>

                if (_repositoryPuestoPers.TableNoTracking.Where(x => x.I_ID_PERSONA == request.I_PERSON_ID).ToList().Count == 0)
                {
                    var newPuestoPersona = new Puesto_persona
                    {
                        I_ID_PERSONA = request.I_PERSON_ID,
                        I_ID_PUESTO = 2,
                        B_ESTADO = "1",
                        D_FECHA_CREACION = _repositoryDate.Now,
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO
                    };
                    _repositoryPuestoPers.Insert(new List<Puesto_persona> { newPuestoPersona });
                }
            }

            int idCuenta = _repositoryCuenta.TableNoTracking.Where(x => x.I_ID_PERSONA == request.I_PERSON_ID && x.I_ID_TIPO_CUENTA == 2).FirstOrDefault()!.I_ID_CUENTA;


            if (_repositoryTramiteDoc.TableNoTracking.Where(x => x.I_ID_TRAMITE_DOC == 1 && x.B_ESTADO == "1").ToList().Count == 1)
            {
                _repositoryTramiteCuenta.Insert(new List<Tramite_cuenta>
                {
                    new Tramite_cuenta
                    {
                        I_ID_CUENTA = idCuenta,
                        I_ID_TRAMITE_DOC = 1,
                        B_ESTADO = "1",
                        I_USUARIO_CREACION =usuario.I_ID_USUARIO,
                        D_FECHA_CREACION = _repositoryDate.Now
                    }
                });
            }

            if (_repositoryCredito.TableNoTracking.Where(x => x.I_ID_CUENTA == idCuenta && x.B_ESTADO == "1").ToList().Count != 0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 400,
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "El cliente tiene un crédito pendiente")
                    }
                };
            }

            decimal montoGasto = _repositoryTramiteDoc.Table.FirstOrDefault(x => x.I_ID_TRAMITE_DOC == 1 && x.B_ESTADO == "1")!.I_TARIFA;

            var credito = new Credito
            {
                I_ID_CUENTA = idCuenta,
                I_ID_FREC_PAGO = Convert.ToInt32(request.V_ID_PAYMENT_FREQUENCY),
                I_ID_TIPO_CREDITO = int.Parse(request.V_ID_TYPE_CREDIT),
                I_MONTO_PRESTAMO = decimal.Parse(request.V_LOAN_AMOUNT),
                I_PLAZO_CANTIDAD = int.Parse(request.V_TERM_QUANTITY),
                I_ID_INTERES_CREDITO = request.I_INTEREST_CREDIT_ID,
                D_FECHA_DESEMBOLSO = Convert.ToDateTime(request.D_DISBURSEMENT_DATE),
                I_GASTO_FINANCIERO = Math.Round(Convert.ToDecimal(request.V_LOAN_AMOUNT) * (montoGasto / 100), 2),
                I_MONTO_REAL = Convert.ToDecimal(request.V_LOAN_AMOUNT) - Math.Round(Convert.ToDecimal(request.V_LOAN_AMOUNT) * (montoGasto / 100), 2),
                B_ESTADO = "1",
                D_FECHA_CREACION = _repositoryDate.Now,
                I_USUARIO_CREACION = usuario.I_ID_USUARIO,
            };

            _repositoryCredito.Insert(credito);

            // Calculos
            decimal montoPrestamo = credito.I_MONTO_PRESTAMO;
            decimal tasaInteres = interesCredito / 100;

            int plazo = credito.I_PLAZO_CANTIDAD;

            double montoCuotaDouble = (double)montoPrestamo * ((double)tasaInteres * Math.Pow(1 + (double)tasaInteres, plazo)) / (Math.Pow(1 + (double)tasaInteres, plazo) - 1);

            decimal montoCuota = Convert.ToDecimal(montoCuotaDouble);

            decimal saldo = montoPrestamo;

            // Cuotas
            var cuotas = Enumerable.Range(0, plazo).Select(i =>
            {
                decimal interes = saldo * tasaInteres;
                decimal capital = montoCuota - interes;
                saldo -= capital;

                DateTime fechaPago;

                switch (credito.I_ID_FREC_PAGO)
                {
                    case 1:
                        fechaPago = credito.D_FECHA_DESEMBOLSO.AddDays(i + 1);
                        break;
                    case 2:
                        fechaPago = credito.D_FECHA_DESEMBOLSO.AddDays((i + 1) * 7);
                        break;
                    case 3:
                    default:
                        fechaPago = credito.D_FECHA_DESEMBOLSO.AddMonths(i + 1);
                        break;
                }

                return new Cuota
                {

                    I_ID_CREDITO = credito.I_ID_CREDITO,
                    V_NUMERO_CUOTA = (i + 1).ToString(),
                    I_MONTO_CUOTA = Math.Round(montoCuota, 2),
                    I_CAPITAL = Math.Round(capital, 2),
                    I_SALDO_INICIAL = Math.Round(saldo, 2),
                    I_INTERES = Math.Round(interes, 2),
                    I_SALDO_FINAL = Math.Round(montoCuota - interes, 2),
                    D_FECHA_PAGO = fechaPago,
                    B_ESTADO = "1",
                    D_FECHA_CREACION = _repositoryDate.Now,
                    I_MONTO_TOTAL = Math.Round(montoCuota, 2),
                };
            }).ToList();

            _repositoryCuota.Insert(cuotas);

            /// <summary>
            /// Insertando operaciones
            /// </summary>
            var ultimaOperacion = _repositoryOperacion.TableNoTracking.OrderByDescending(x => x.I_ID_OPERACION).FirstOrDefault();
            int numOperacion = 0;

            if (ultimaOperacion is null)
            {
                numOperacion = 1;
            }
            else
            {
                numOperacion = Convert.ToInt32(ultimaOperacion.V_NUMERO_OPERACION) + 1;
            }

            var newOperacion = new Operacion
            {
                I_ID_CUENTA = idCuenta,
                I_ID_TIPO_OPERACION = 6,
                V_NUMERO_OPERACION = Convert.ToString(numOperacion),
                I_MONTO = Math.Round(Convert.ToDecimal(request.V_LOAN_AMOUNT) * (montoGasto / 100), 2),
                B_ESTADO = "1",
                D_FECHA_CREACION = _repositoryDate.Now,
                I_USUARIO_CREACION = usuario.I_ID_USUARIO
            };
            _repositoryOperacion.Insert(new List<Operacion> { newOperacion });

            /// <summary>
            /// Actualizando la columna saldo de cuenta
            /// </summary>
            var updateCuenta = _repositoryCuenta.Table.FirstOrDefault(x => x.I_ID_CUENTA == idCuenta && x.I_ID_TIPO_CUENTA == 2 && x.B_ESTADO == "1");
            updateCuenta!.I_SALDO = (decimal)montoCuota * Convert.ToInt32(request.V_TERM_QUANTITY);
            _repositoryCuenta.Update(updateCuenta);


            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);


        }
    }
}
