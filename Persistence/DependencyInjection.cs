using Shared.Support.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Application.Common.Interfaces;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.Configure<DbConnectionConfig>(options => configuration.GetSection("DbConnection").Bind(options));

            services.AddDbContext<ApplicationContext>();

            services.AddScoped<IApplicationContext>(provider => provider.GetService<ApplicationContext>());

            return services;
        }
    }
}
