using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.RapiDiario;
using HRA.Domain.Entities.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace HRA.Application.UseCases.Operacion_.Commands.NewOperacion
{
    public class NewOperacionHandler : IRequestHandler<NewOperacionVM, Iresult>
    {
        private readonly IRepository<Operacion> _repositoryOperacion;
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Cuenta> _repositoryCuenta;
        private readonly IRepository<Credito> _repositoryCredito;
        private readonly IRepository<Cuota> _repositoryCuota;
        private readonly IRepository<Sub_cuota> _repositorySubCuota;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccesor;
        private readonly IUnitOfWork _unitOfWork;

        public NewOperacionHandler(
            IRepository<Operacion> operacionRepository,
            IRepository<Credito> creditoRepository,
            IRepository<Cuota> cuotaRepository,
            IRepository<Usuario> usuarioRepository,
            IRepository<Cuenta> cuentaRepository,
            IRepository<Sub_cuota> subCuotaRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccesor)
        {
            _repositoryOperacion = operacionRepository;
            _repositoryUsuario = usuarioRepository;
            _repositoryCuenta = cuentaRepository;
            _repositoryCredito = creditoRepository;
            _repositoryCuota = cuotaRepository;
            _repositorySubCuota = subCuotaRepository;
            _repositoryDate = dateTime;
            _httpContextAccesor = httpContextAccesor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(NewOperacionVM request, CancellationToken cancellationToken)
            {
            var claims = _httpContextAccesor?.HttpContext?.User?.Claims;
            var claimUserId = claims?.FirstOrDefault(c => c.Type == "IDUser")?.Value;

            var usuario = _repositoryUsuario.TableNoTracking
                .Where(x => x.B_ESTADO == "1" && x.I_ID_USUARIO == Convert.ToInt32(claimUserId)).FirstOrDefault();

            if (usuario is null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    //StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("01", "Usuario no autorizado")
                    }
                };
            }

            var cuenta = _repositoryCuenta.Table.FirstOrDefault(x => x.I_ID_CUENTA == int.Parse(request.V_ID_ACCOUNT));

            if (cuenta is null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    //StatusCode = 500,
                    Value = new List<DetailError>()
                    {
                        new DetailError("02", "La cuenta no existe o está desactivada.")
                    }
                };
            }


            // Cuenta ahorros
            var cuentaAhorros = _repositoryCuenta.Table.FirstOrDefault(x => x.I_ID_TIPO_CUENTA == 1 && x.I_ID_CUENTA == int.Parse(request.V_ID_ACCOUNT));

            // Cuenta aportes
            var cuentaAportes = _repositoryCuenta.Table.FirstOrDefault(x => x.I_ID_TIPO_CUENTA == 3 && x.I_ID_CUENTA == int.Parse(request.V_ID_ACCOUNT));

            // Cuenta crédito
            var cuentaCredito = _repositoryCuenta.Table.FirstOrDefault(x => x.I_ID_TIPO_CUENTA == 2 && x.I_ID_CUENTA == int.Parse(request.V_ID_ACCOUNT));

            var monto = Convert.ToDecimal(request.V_AMOUNT, CultureInfo.InvariantCulture);

            if (int.Parse( request.V_ID_TYPE_OPERATION) == 1 && cuentaAhorros != null)
            {   

                if (monto >= cuentaAhorros.I_SALDO)
                {
                    return new FailureResult<IEnumerable<DetailError>>()
                    {
                        Value = new List<DetailError>()
                        {
                            new DetailError("04", "Saldo insuficiente.")
                        }
                    };
                }
                cuentaAhorros.I_SALDO -= monto;
                // Crear operacion
                var operacion = new Operacion
                {
                    I_ID_CUENTA = int.Parse(request.V_ID_ACCOUNT),
                    I_ID_CUOTA = !string.IsNullOrEmpty(request.V_ID_CUOTA) ? int.Parse(request.V_ID_CUOTA) : (int?)null,
                    V_NUMERO_OPERACION = "1",
                    I_MONTO = decimal.Parse(request.V_AMOUNT),
                    I_ID_TIPO_OPERACION =int.Parse(request.V_ID_TYPE_OPERATION),
                    B_ESTADO = "1",
                    D_FECHA_CREACION = _repositoryDate.Now,
                    I_USUARIO_CREACION = usuario.I_ID_USUARIO
                };

                _repositoryOperacion.Insert(operacion);
                _repositoryCuenta.Update(cuentaAhorros);
            }


            if(int.Parse(request.V_ID_TYPE_OPERATION) == 2 && cuentaAhorros != null)
            {
                cuentaAhorros.I_SALDO += monto;
                // Crear operacion
                var operacion = new Operacion
                {
                    I_ID_CUENTA = int.Parse(request.V_ID_ACCOUNT),
                    I_ID_CUOTA = !string.IsNullOrEmpty(request.V_ID_CUOTA) ? int.Parse(request.V_ID_CUOTA) : (int?)null,
                    V_NUMERO_OPERACION = "1",
                    I_MONTO = decimal.Parse(request.V_AMOUNT),
                    I_ID_TIPO_OPERACION =int.Parse( request.V_ID_TYPE_OPERATION),
                    B_ESTADO = "1",
                    D_FECHA_CREACION = _repositoryDate.Now,
                    I_USUARIO_CREACION = usuario.I_ID_USUARIO
                };

                _repositoryOperacion.Insert(operacion);

                _repositoryCuenta.Update(cuentaAhorros);
            }


            //Para la cuenta aportes
            if(int.Parse(request.V_ID_TYPE_OPERATION) == 3 && cuentaAportes != null ) 
            {
                DateTime fechaActual = DateTime.Now.Date;

                var aporteMes = _repositoryOperacion.Table.FirstOrDefault(x => x.I_ID_TIPO_OPERACION == 3 && x.D_FECHA_CREACION == fechaActual);

                //verificar si el aporte ya se hizo este mes
                if (aporteMes != null)
                {
                    cuentaAportes.I_SALDO += monto;

                    // Crear operacion
                    var operacion = new Operacion
                    {
                        I_ID_CUENTA = int.Parse(request.V_ID_ACCOUNT),
                        I_ID_CUOTA = !string.IsNullOrEmpty(request.V_ID_CUOTA) ? int.Parse(request.V_ID_CUOTA) : (int?)null,
                        V_NUMERO_OPERACION = "1",
                        I_MONTO = decimal.Parse(request.V_AMOUNT),
                        I_ID_TIPO_OPERACION =int.Parse( request.V_ID_TYPE_OPERATION),
                        B_ESTADO = "1",
                        D_FECHA_CREACION = _repositoryDate.Now,
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO
                    };

                    _repositoryOperacion.Insert(operacion);

                    _repositoryCuenta.Update(cuentaAportes);

                }
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("05", "El aporte del presente mes ya existe")
                    }
                };
            }

            //Pago de cuotas

            if (int.Parse(request.V_ID_TYPE_OPERATION) == 4 && cuentaCredito != null && request.V_ID_CUOTA!=null)
            {
                // convertir cadena de montos y arreglo de montos
                
                int[] idsCuota = request.V_ID_CUOTA.Split(',').Select(s => int.Parse(s)).ToArray();
                int idCredito = 0;
                foreach (var idCuota in idsCuota)
                {
                    var cuota = _repositoryCuota.Table.FirstOrDefault(x => x.I_ID_CUOTA == idCuota && x.B_ESTADO == "1");
                    idCredito = cuota.I_ID_CREDITO;
                    var subCuota = _repositorySubCuota.Table.FirstOrDefault(x => x.I_ID_CUOTA == idCuota && x.B_ESTADO == "1");

                    if (subCuota != null)
                    {
                        return new FailureResult<IEnumerable<DetailError>>()
                        {
                            Value = new List<DetailError>()
                            {
                                new DetailError("04", "La cuota se está pagando en partes.")
                            }
                        };
                    }

                    if (cuota == null)
                    {
                        return new FailureResult<IEnumerable<DetailError>>()
                        {
                            Value = new List<DetailError>()
                            {
                                new DetailError("04", "La Cuota no existe.")
                            }
                        };
                    }

                    if (cuota.I_MONTO_TOTAL != monto)
                    {
                        return new FailureResult<IEnumerable<DetailError>>()
                        {
                            Value = new List<DetailError>()
                            {
                                new DetailError("04", "El monto ingresado no coincide con el de la cuota.")
                            }
                        };
                    }
                }

                var operaciones = idsCuota.Select(id =>
                {
                    var operacion = new Operacion
                    {
                        I_ID_CUENTA = int.Parse(request.V_ID_ACCOUNT),
                        I_ID_CUOTA = id,
                        V_NUMERO_OPERACION = "1",
                        I_MONTO = decimal.Parse(request.V_AMOUNT,CultureInfo.InvariantCulture),
                        I_ID_TIPO_OPERACION = int.Parse(request.V_ID_TYPE_OPERATION),
                        B_ESTADO = "1",
                        D_FECHA_CREACION = _repositoryDate.Now,
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO
                    };

                    return operacion;

                }).ToList();

                _repositoryOperacion.Insert(operaciones);

                foreach (var idCuota in idsCuota)
                {
                    /// <summary>
                    /// Actualizando la columna saldo de cuenta
                    /// </summary>
                    decimal montoCuota = _repositoryCuota.TableNoTracking.Where(x => x.I_ID_CUOTA == idCuota).FirstOrDefault()!.I_MONTO_TOTAL;
                    decimal montoMora = _repositoryCuota.TableNoTracking.Where(x => x.I_ID_CUOTA == idCuota).FirstOrDefault()!.I_MONTO_TOTAL - montoCuota;

                    var updateCuenta = _repositoryCuenta.Table.FirstOrDefault(x => x.I_ID_CUENTA == Convert.ToInt32(request.V_ID_ACCOUNT) && x.I_ID_TIPO_CUENTA==2 && x.B_ESTADO == "1");
                    updateCuenta!.I_SALDO = (updateCuenta.I_SALDO - montoCuota) + montoMora;
                    _repositoryCuenta.Update(updateCuenta);

                    var n = _repositoryCuota.TableNoTracking.Where(x => x.I_ID_CREDITO==idCredito && x.B_ESTADO == "1").ToList();


                    if (_repositoryCuota.TableNoTracking.Where(x => x.I_ID_CREDITO == idCredito && x.B_ESTADO == "1").ToList().Count==1)
                    {
                        /// <summary>
                        /// Actualizando tabla credito
                        /// </summary>

                        var credito = _repositoryCredito.Table.FirstOrDefault(x => x.I_ID_CUENTA == Convert.ToInt32(request.V_ID_ACCOUNT) && x.B_ESTADO=="1");
                        credito!.B_ESTADO = "2";
                        credito.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                        credito.D_FECHA_MODIFICA = _repositoryDate.Now;
                        _repositoryCredito.Update(credito);

                    }

                    var cuota = _repositoryCuota.Table.FirstOrDefault(x => x.I_ID_CUOTA == idCuota && x.B_ESTADO == "1");
                    cuota!.B_ESTADO = "2";
                    cuota.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                    cuota.D_FECHA_MODIFICA = _repositoryDate.Now;
                    _repositoryCuota.Update(cuota);
                }

            }

            // Pago sub Cuotas
            if (int.Parse(request.V_ID_TYPE_OPERATION) == 5 && cuentaCredito != null && request.V_ID_CUOTA != null)
            {
                decimal sumaSubCuota = _repositorySubCuota.Table
                                    .Where(x => x.I_ID_CUOTA == int.Parse(request.V_ID_CUOTA) && x.B_ESTADO == "1")
                                    .Sum(x => x.I_MONTO);

                var cuota = _repositoryCuota.Table.FirstOrDefault(x => x.I_ID_CUOTA == int.Parse(request.V_ID_CUOTA) && x.B_ESTADO == "1");

                if ((sumaSubCuota + Convert.ToDecimal(request.V_AMOUNT))> cuota.I_MONTO_TOTAL)
                {                 

                    return new FailureResult<IEnumerable<DetailError>>()
                    {
                        Value = new List<DetailError>()
                        {
                            new DetailError("05", "El monto ingresado excede a la cuota.")
                        }
                    };

                }
                // Crear operacion
                var operacion = new Operacion
                {
                    I_ID_CUENTA = int.Parse(request.V_ID_ACCOUNT),
                    I_ID_CUOTA = !string.IsNullOrEmpty(request.V_ID_CUOTA) ? int.Parse(request.V_ID_CUOTA) : (int?)null,
                    V_NUMERO_OPERACION = "1",
                    I_MONTO = decimal.Parse(request.V_AMOUNT),
                    I_ID_TIPO_OPERACION = int.Parse(request.V_ID_TYPE_OPERATION),
                    B_ESTADO = "1",
                    D_FECHA_CREACION = _repositoryDate.Now,
                    I_USUARIO_CREACION = usuario.I_ID_USUARIO
                };

                var nuevaSubCuota = new Sub_cuota
                {
                    I_MONTO = Convert.ToDecimal(request.V_AMOUNT),
                    I_ID_CUOTA = int.Parse(request.V_ID_CUOTA),
                    I_SALDO_CUOTA = cuota.I_MONTO_TOTAL - sumaSubCuota -operacion.I_MONTO,
                    B_ESTADO = "1",
                    D_FECHA_CREACION = _repositoryDate.Now,
                    I_USUARIO_CREACION = usuario.I_ID_USUARIO
                };
                

                if (nuevaSubCuota.I_SALDO_CUOTA == 0)
                {

                    /// <summary>
                    /// Actualizando la columna saldo de cuenta
                    /// </summary>
                    decimal montoCuota = _repositoryCuota.TableNoTracking.Where(x => x.I_ID_CUOTA == Convert.ToInt32(request.V_ID_CUOTA)).FirstOrDefault()!.I_MONTO_TOTAL;
                    decimal montoMora = _repositoryCuota.TableNoTracking.Where(x => x.I_ID_CUOTA == Convert.ToInt32(request.V_ID_CUOTA)).FirstOrDefault()!.I_MONTO_TOTAL - montoCuota;

                    var updateCuenta = _repositoryCuenta.Table.FirstOrDefault(x => x.I_ID_CUENTA == Convert.ToInt32(request.V_ID_ACCOUNT) && x.I_ID_TIPO_CUENTA == 2 && x.B_ESTADO == "1");
                    updateCuenta!.I_SALDO = (updateCuenta.I_SALDO - montoCuota) + montoMora;
                    _repositoryCuenta.Update(updateCuenta);

                    /// <summary>
                    /// Actualizando tabla cuota
                    /// </summary>
                    cuota.B_ESTADO = "2";
                    cuota.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                    cuota.D_FECHA_MODIFICA = _repositoryDate.Now;
                    _repositoryCuota.Update(cuota);
                    
                    if (_repositoryCuota.TableNoTracking.Where(x => x.I_ID_CUOTA == Convert.ToInt32(request.V_ID_CUOTA) && x.B_ESTADO == "1").ToList().Count ==0 )
                    {
                        /// <summary>
                        /// Actualizando tabla credito
                        /// </summary>

                        var credito = _repositoryCredito.Table.FirstOrDefault(x => x.I_ID_CUENTA == Convert.ToInt32(request.V_ID_ACCOUNT) && x.B_ESTADO == "1");
                        credito!.B_ESTADO = "2";
                        credito.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                        credito.D_FECHA_MODIFICA = _repositoryDate.Now;
                        _repositoryCredito.Update(credito);

                    }
                    
                }

                _repositoryOperacion.Insert(operacion);
                _repositorySubCuota.Insert(nuevaSubCuota);
            }

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }
    }
}