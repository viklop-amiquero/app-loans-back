using HRA.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HRA.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : MediatR.IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService,
        IHttpContextAccessor httpContextAccessor)
    {
        _timer = new Stopwatch();
        _httpContextAccessor = httpContextAccessor;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).FullName;
            var userIdClaim = _httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;
            var userId = string.IsNullOrEmpty(userIdClaim) ? "Login" : userIdClaim;

            var userNameClaim = _httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "User_name")?.Value;
            var userName = string.IsNullOrEmpty(userNameClaim) ? request.GetType().GetProperty("V_USER")?.GetValue(request) : userNameClaim;

            _logger.LogWarning("Performance Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) - {@UserId} - {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }

        return response;
    }
}
