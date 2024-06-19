using FluentValidation.AspNetCore;
using HRA.Application.Common.Interfaces;
using HRA.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using HRA.WebAPI.Filters;

namespace HRA.WebAPI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            //services.AddHealthChecks()
            //    .AddDbContextCheck<ApplicationDbContext>();

            //services.AddControllersWithViews(options =>
            //    options.Filters.Add<ApiExceptionFilterAttribute>())
            //        .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

            services.AddScoped<ValidationFilterAttribute>();

            services.AddMvcCore(options =>
                options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);



            return services;
        }
    }
}
