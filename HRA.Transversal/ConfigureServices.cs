using HRA.Transversal.Interfaces;
using HRA.Transversal.mail_provider;
using HRA.Transversal.Models;
using HRA.Transversal.Security;
using HRA.Transversal.serviceProvider;
using HRA.Transversal.tokenProvider;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using System.Text.Json;

namespace HRA.Transversal
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddTransversalServices(this IServiceCollection services, IConfiguration configuration, ConfigureHostBuilder host)
        {
            #region AppSettings

            // Valores para la JWT
            services.Configure<Jwt>(configuration.GetSection(nameof(Jwt)));
            services.AddSingleton<IAuthenticationJWT>(sp => sp.GetRequiredService<IOptions<Jwt>>().Value);

            // Valores del servicio de RENIEC de PIDE
            services.Configure<Pide_Reniec>(configuration.GetSection(nameof(Pide_Reniec)));
            services.AddSingleton<IPide_Reniec>(sp => sp.GetRequiredService<IOptions<Pide_Reniec>>().Value);

            // Valores del servicio de RENIEC de MINSA
            services.Configure<Minsa_Reniec>(configuration.GetSection(nameof(Minsa_Reniec)));
            services.AddSingleton<IMinsa_Reniec>(sp => sp.GetRequiredService<IOptions<Minsa_Reniec>>().Value);

            // Valores del servicio de AIRHSP
            services.Configure<Airhsp>(configuration.GetSection(nameof(Airhsp)));
            services.AddSingleton<IAirhsp>(sp => sp.GetRequiredService<IOptions<Airhsp>>().Value);

            // Valores del servicio de SIS
            services.Configure<Sis>(configuration.GetSection(nameof(Sis)));
            services.AddSingleton<ISis>(sp => sp.GetRequiredService<IOptions<Sis>>().Value);

            // Valores del servicio de Send_mail
            services.Configure<Credentials>(configuration.GetSection(nameof(Credentials)));
            services.AddSingleton<ICredential>(sp => sp.GetRequiredService<IOptions<Credentials>>().Value);

            services.AddSingleton<GenerateToken>();
            services.AddSingleton<ProviderAPI>();
            services.AddSingleton<Isend_mail, send_mail>();

            services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();


            #endregion

            #region JWT_tokens

            var settings = configuration.GetSection("Jwt").Get<Jwt>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Configuración de TokenValidationParameters
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key.ToString())),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

                // Eventos JwtBearer para el manejo de excepciones
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";

                        await HandleAuthenticationFailure(context);
                    }
                };
            });

            // Método para manejar el fallo de autenticación
            async Task HandleAuthenticationFailure(JwtBearerChallengeContext context)
            {
                var user = context.HttpContext.User;
                // Lógica para identificar y manejar diferentes tipos de excepciones
                if (context.AuthenticateFailure is SecurityTokenExpiredException expiredException)
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                    context.Response.Headers.Add("Access-Control-Expose-Headers", "Token-Expired");
                    context.Response.Headers.Add("x-token-expired", expiredException.Expires.ToString("dd/MM/yyyy hh:mm"));
                    context.ErrorDescription = $"El token ha expirado";
                }
                else if (context.AuthenticateFailure is SecurityTokenInvalidSignatureException ||
                         context.AuthenticateFailure is SecurityTokenValidationException ||
                         context.AuthenticateFailure is SecurityTokenSignatureKeyNotFoundException)
                {
                    context.Response.Headers.Add("Token-Invalido", "true");
                    context.Error = Convert.ToString((int)HttpStatusCode.Unauthorized);
                    context.ErrorDescription = "No es un token válido";
                }
                else if (context.AuthenticateFailure == null)
                {
                    context.ErrorDescription = "Se requiere que se proporcione un token de acceso";
                    context.Error = Convert.ToString((int)HttpStatusCode.Unauthorized);
                    context.ErrorDescription = "Token no ingresado";
                }

                // Envía una respuesta con el mensaje de error
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    hasSucceeded = false,
                    value = new
                    {
                        errorCode = context.Error,
                        message = context.ErrorDescription
                    }
                }));
            }

            services.AddAuthorization(options =>
            {
                options.AddCustomPolicies();
            });


            #endregion

            #region Config_Cors

            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowWebApp", det =>
                {
                    det.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });


            #endregion

            #region Claims_value

            //services.AddSingleton<IHttpContextAccessor, ClaimContextAccessor>();

            #endregion

            return services;
        }
    }
}