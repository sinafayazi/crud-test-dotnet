using CustomerService.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Api.Extensions.DependencyInjection
{
    public static class DatabaseInjection
    {
        public static IServiceCollection AddConfiguredDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Data Context
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DbConnection")));

            return services;
        }
    }
}