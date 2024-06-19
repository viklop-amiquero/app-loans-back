using HRA.Application.Common.Interfaces;
using HRA.Infrastructure.Persintence;
using HRA.Infrastructure.Persintence.Interfaces;
using HRA.Infrastructure.Persistence.Interceptors;
using HRA.Infrastructure.Persistence.Interfaces;
using HRA.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HRA.Infrastructure.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<AuditableEntitySaveChangesInterceptor>();

            #region Aplicacion_principal
            services.AddDbContext<ApplicationDbContext>(options =>
                           options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                               builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(EntityFrameworkRepository<>));
            #endregion

            #region Aplicacion_Gale_Sigh
            services.AddDbContext<ApplicationGaleSighDbContext>(options =>
                          options.UseSqlServer(configuration.GetConnectionString("GaleConnection"),
                              builder => builder.MigrationsAssembly(typeof(ApplicationGaleSighDbContext).Assembly.FullName)));

            services.AddScoped<IDbContextGaleSigh>(provider => provider.GetRequiredService<ApplicationGaleSighDbContext>());
            services.AddScoped<IUnitOfWorkGaleSigh, UnitOfWorkGaleSigh>();
            services.AddScoped(typeof(IRepositoryGaleSigh<>), typeof(EntityGaleSighFrameworkRepository<>));
            #endregion

            //services.AddTransient<IDateTime, DateTimeService>();
            services.AddSingleton<IDateTime, DateTimeService>();


            return services;
        }
    }
}
