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
            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
