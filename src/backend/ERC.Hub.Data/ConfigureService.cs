using ERC.Hub.Data.Configurations;
using ERC.Hub.Data.Context;
using ERC.Hub.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ERC.Hub.Data
{
    public static class ConfigureService
    {
        public static IServiceCollection AddDataService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditTrailInterceptor>();
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                var auditLogs = provider.GetRequiredService<AuditTrailInterceptor>();
                options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                    .AddInterceptors(auditLogs);
            });
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
