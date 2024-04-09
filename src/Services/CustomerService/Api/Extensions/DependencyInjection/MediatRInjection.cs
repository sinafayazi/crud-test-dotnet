using System.Reflection;
using Communal.Application.Infrastructure.Operations;
using CustomerService.Application.Behaviors.Common;
using CustomerService.Application.Behaviors.Customers;
using CustomerService.Application.Models.Commands.Customers;
using MediatR;

namespace CustomerService.Api.Extensions.DependencyInjection
{
    public static class MediatRInjection
    {
        public static IServiceCollection AddConfiguredMediatR(this IServiceCollection services)
        {
            // Handlers
            services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);

            // Generic behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommitBehavior<,>));

            // Validation behaviors
            services.AddTransient(typeof(IPipelineBehavior<CreateCustomerCommand, OperationResult>),
                typeof(CreateCustomerValidationBehavior<CreateCustomerCommand, OperationResult>));
            services.AddTransient(typeof(IPipelineBehavior<UpdateCustomerCommand, OperationResult>),
                typeof(UpdateCustomerValidationBehavior<UpdateCustomerCommand, OperationResult>));

            return services;
        }
    }
}