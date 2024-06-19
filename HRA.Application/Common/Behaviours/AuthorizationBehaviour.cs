using HRA.Application.Common.Exceptions;
using HRA.Application.Common.Interfaces;
using HRA.Domain.Entities.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace HRA.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthorizationService _authorizationService;
    private readonly IRepository<Rol> _repositoryRol;

    public AuthorizationBehaviour(
        IRepository<Rol> rolRepository,
        IHttpContextAccessor httpContextAccessor,
        IAuthorizationService authorizationService)
    {
        _repositoryRol = rolRepository;
        _httpContextAccessor = httpContextAccessor;
        _authorizationService = authorizationService;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var requestName = request.GetType().Namespace;
        var roles = httpContext?.User?.FindAll(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(c => c.Value).ToList() ?? new List<string>();

        if (request.GetType().GetProperty("Authorize") != null)
        {
            if ((bool)request.GetType().GetProperty("Authorize")?.GetValue(request))
            {
                return await next();
            }
        }

        // Verificar si es una solicitud de inicio de sesión
        if (roles.Count == 0 && requestName == "HRA.Application.UseCases.Login_.Queries.Autentication")
        {
            return await next();
        }

        // Si no tiene roles
        if (roles.Count == 0)
        {
            throw new UnauthorizedAccessException();
        }

        // Role-based authorization
        var authorized = _repositoryRol.TableNoTracking.Any(w => roles.Contains(w.V_ROL));

        // Must be a member of at least one role in roles
        if (!authorized)
        {
            throw new ForbiddenAccessException();
        }

        return await next();
    }
}

