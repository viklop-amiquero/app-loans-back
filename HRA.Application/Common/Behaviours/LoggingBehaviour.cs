using HRA.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PNB.NET.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoggingBehaviour(ILogger<TRequest> logger, 
        ICurrentUserService currentUserService,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).FullName;
        var userIdClaim = _httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "IDUser")?.Value;
        var userId = string.IsNullOrEmpty(userIdClaim) ? "Login" : userIdClaim;

        var userNameClaim = _httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == "User_name")?.Value;
        var userName = string.IsNullOrEmpty(userNameClaim) ? request.GetType().GetProperty("V_USER")?.GetValue(request) : userNameClaim;

        _logger.LogInformation("Logging Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}
