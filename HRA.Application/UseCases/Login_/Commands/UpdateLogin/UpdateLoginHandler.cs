using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Login_.Commands.UpdateLogin
{
    public class UpdateLoginHandler : IRequestHandler<UpdateLoginVM, Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Clave> _repositoryClave;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLoginHandler(
            IRepository<Usuario> usuarioRepository,
            IRepository<Clave> passwordRepository, 
            IUnitOfWork unitOfWork, 
            IDateTime dateTime, 
            IHttpContextAccessor httpContextAccessor)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryClave = passwordRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<Iresult> Handle(UpdateLoginVM request, CancellationToken cancellationToken)
        {
            //var identity = _httpContextAccessor as ClaimContextAccessor;

            //if (identity.IDUser != request.I_USER_ID && identity.UserName != identity.IDUser)
            //{
            //    return new FailureResult<IEnumerable<DetailError>>()
            //    {
            //        Value = new List<DetailError>()
            //        {
            //            new DetailError("05","No autorizado")
            //        }
            //    };
            //}

            var Claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var claimUserId = Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

            var usuario = _repositoryUsuario.TableNoTracking.FirstOrDefault(x => x.I_ID_USUARIO.ToString() == request.V_ID_USER && x.B_ESTADO == "1");

            if (usuario is null || Convert.ToInt32(claimUserId) != usuario.I_ID_USUARIO)
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

            object[] password = {
                usuario.V_USUARIO,
                request.V_PASSWORD
            };

            //object[] query = {
            //    request.I_USER_ID,
            //    request.V_PASSWORD
            //};

            object[] parameters = {
                request.V_ID_USER,
                request.V_NEW_PASSWORD
            };

            var entity = await _unitOfWork.ExcuteStoreQuery<entity_Login>("[seguridad].[USP_CONSULTA_ACCESO] {0}, {1}", password);

            //if (sp_login.Count == 0)
            if (entity.Count == 0)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","Contraseña incorrecta")
                    }
                };
            }

            //var entity = await _unitOfWork.ExcuteStoreQuery<entity_Past_pass>("[seguridad].[USP_SEL_QUERY_PAST_PASSWORD] {0}, {1}", query);

            //if (entity == null)
            //{
            //    return new FailureResult<IEnumerable<DetailError>>()
            //    {
            //        Value = new List<DetailError>()
            //        {
            //            new DetailError("02", "No encontrado")
            //        }
            //    };
            //}

            var coincidencia = await _unitOfWork.ExcuteStoreQuery<entity_Past_pass>("[seguridad].[USP_SEL_QUERY_PAST_PASSWORD] {0}, {1}", parameters);

            if (coincidencia.FirstOrDefault()?.I_COINCIDENCIA >= 1)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("06", "Hay coincidencia. Contraseña ya usada")
                    }
                };
            }

            if (coincidencia.Count == 0)
            {
                var new_clave = await _unitOfWork.StoreQuery<entity_Encrypt>("SELECT [seguridad].[UFN_ENCRYPT]({0}) AS B_CHAIN", request.V_NEW_PASSWORD);
                
                var clave_actual = _repositoryClave.Table.OrderByDescending(x => x.D_FECHA_CREACION).FirstOrDefault(x => x.I_ID_USUARIO == entity.First().I_ID_USUARIO && x.B_ESTADO == "1");
                clave_actual.B_ESTADO = "0";
                clave_actual.I_USUARIO_MODIFICA = usuario.I_ID_USUARIO;
                clave_actual.D_FECHA_MODIFICA = _repositoryDate.Now;

                _repositoryClave.Insert(new List<Clave>
                {
                    new Clave
                    {
                        I_ID_USUARIO = usuario.I_ID_USUARIO,
                        V_CLAVE_HASH = new_clave.FirstOrDefault().B_CHAIN,
                        B_ESTADO = "1",
                        I_USUARIO_CREACION = usuario.I_ID_USUARIO,
                        D_FECHA_CREACION = _repositoryDate.Now
                    }
                });

                await _unitOfWork.CommitChanges();
                return new SuccessResult<Unit>(Unit.Value);
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
