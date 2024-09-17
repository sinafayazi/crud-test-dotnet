using CustomerService.Application.Interfaces;
using CustomerService.Persistence;

namespace CustomerService.Api.Extensions.DependencyInjection
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}