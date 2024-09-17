using Communal.Api.HealthChecks;

namespace CustomerService.Api.Extensions.DependencyInjection
{
    public static class HealthCheckInjection
    {
        public static IServiceCollection AddConfiguredHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<GeneralHealthCheck>("Customer-check");

            return services;
        }
    }
}