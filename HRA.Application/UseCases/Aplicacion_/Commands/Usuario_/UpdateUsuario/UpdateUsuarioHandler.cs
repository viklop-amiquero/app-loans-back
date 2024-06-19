using AutoMapper;
using HRA.Application.Common.Interfaces;
using HRA.Application.Common.Models;
using HRA.Domain.Entities.Bussines;
using HRA.Domain.Entities.Security;
using HRA.Domain.EntitiesStoreProcedure;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.UseCases.Aplicacion_.Commands.Usuario_.UpdateUsuario
{
    public class UpdateUsuarioHandler : IRequestHandler<UpdateUsuarioVM,Iresult>
    {
        private readonly IRepository<Usuario> _repositoryUsuario;
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Clave> _repositoryClave;
        private readonly IDateTime _repositoryDate;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUsuarioHandler (
            IRepository<Usuario> usuarioRepository,
            IRepository<Persona> personaRepository,
            IRepository<Clave> claveRepository,
            IUnitOfWork unitOfWork,
            IDateTime dateTime,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repositoryUsuario = usuarioRepository;
            _repositoryPersona = personaRepository;
            _repositoryClave = claveRepository;
            _repositoryDate = dateTime;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<Iresult> Handle(UpdateUsuarioVM request, CancellationToken cancellationToken)
        {

            var Claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var claimUserId = Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;

            //var usuario = _repositoryPersona.TableNoTracking
            //    .Where(x => x.B_ESTADO == "1" && x.V_DNI == request.V_NAME).FirstOrDefault();

            //if (usuario is null)
            //{
            //    return new FailureResult<IEnumerable<DetailError>>()
            //    {
            //        StatusCode = 406,
            //        Value = new List<DetailError>()
            //        {
            //            new DetailError("03", "Usuario no esta activo o no coresponde")
            //        }
            //    };
            //}

            //var entity = _repositoryClave.Table.Where(x => x.I_ID_USUARIO == Convert.ToInt32(claimUserId) && x.B_ESTADO == "1").FirstOrDefault();
            var entity = _repositoryClave.Table.FirstOrDefault(x => x.I_ID_USUARIO == Convert.ToInt32(claimUserId) && x.B_ESTADO == "1");

            if (entity == null)
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    Value = new List<DetailError>()
                    {
                        new DetailError("02","No existe el puesto o ya está activo")
                    }
                };
            }

            var clave_ant = entity.V_CLAVE_HASH;
            var clave_ant_dec = await _unitOfWork.StoreQuery<entity_Decipher>("SELECT [seguridad].[UFN_DECIPHER]({0}) AS V_CHAIN", clave_ant);
            var clave_ant_str = clave_ant_dec.FirstOrDefault().V_CHAIN;

            if (clave_ant_str.Equals(request.V_PASS))
            {
                return new FailureResult<IEnumerable<DetailError>>()
                {
                    StatusCode = 400,
                    Value = new List<DetailError>()
                    {
                        new DetailError("06", "Contraseña ya existente. Ingrese una nueva")
                    }
                };
            }

            entity.B_ESTADO = "0";
            entity.I_USUARIO_MODIFICA = Convert.ToInt32(claimUserId);
            entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            var new_clave = await _unitOfWork.StoreQuery<entity_Encrypt>("SELECT [seguridad].[UFN_ENCRYPT]({0}) AS B_CHAIN", request.V_PASS);
            _repositoryClave.Insert(new List<Clave>
            {
                new Clave
                {
                    I_ID_USUARIO = Convert.ToInt32(claimUserId),
                    V_CLAVE_HASH = new_clave.FirstOrDefault().B_CHAIN,
                    B_ESTADO = "1",
                    I_USUARIO_CREACION = Convert.ToInt32(claimUserId),
                    D_FECHA_CREACION = _repositoryDate.Now
                }
            });

            //entity.V_CLAVE_HASH = new_clave.FirstOrDefault().B_CHAIN;
            //entity.B_ESTADO = "1";
            //entity.I_USUARIO_MODIFICA = Convert.ToInt32(claimUserId);
            //entity.D_FECHA_MODIFICA = _repositoryDate.Now;

            await _unitOfWork.CommitChanges();
            return new SuccessResult<Unit>(Unit.Value);
        }

    }
}
