using Microsoft.AspNetCore.Authorization;

namespace HRA.Transversal.Security
{
    public class CustomClaimRequirement : IAuthorizationRequirement
    {
        public string ClaimType { get; }
        public string ClaimValue { get; }

        public CustomClaimRequirement(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }
    }

    public static class CustomAuthorizationPolicies
    {
        public static void AddCustomPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy("Password_changer", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new CustomClaimRequirement("Change_password", "False"));
            });

            options.AddPolicy("Role_null", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new CustomClaimRequirement("role", ""));
            });

            // Puedes agregar más políticas personalizadas aquí si es necesario
        }
    }

    public class CustomAuthorizationHandler : AuthorizationHandler<CustomClaimRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomClaimRequirement requirement)
        {
            if (requirement.ClaimType == "role")
            {
                if (context.User.Claims.Any(c => c.Type == $"http://schemas.microsoft.com/ws/2008/06/identity/claims/{requirement.ClaimType}" && !string.IsNullOrWhiteSpace(c.Value)))
                {
                    context.Succeed(requirement); // Éxito si hay roles presentes
                }
                else
                {
                    context.Fail(); // Falla si no hay roles presentes
                }
            }
            else
            {
                // Otras lógicas de validación según el CustomClaimRequirement
                if (context.User.HasClaim(c => c.Type == requirement.ClaimType && c.Value == requirement.ClaimValue))
                {
                    context.Succeed(requirement); // Cumple con el requisito
                }
                else
                {

                    context.Fail(); // Falla si el requisito no se cumple
                }
            }
            return Task.CompletedTask;
        }
    }

}
