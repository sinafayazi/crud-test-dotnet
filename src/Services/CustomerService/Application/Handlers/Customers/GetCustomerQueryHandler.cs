using Communal.Application.Infrastructure.Operations;
using CustomerService.Application.Errors;
using CustomerService.Application.Interfaces;
using CustomerService.Application.Mappers;
using CustomerService.Application.Models.Queries.Customers;
using MediatR;

namespace CustomerService.Application.Handlers.Customers
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCustomerQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            // Get
            var entity = await _unitOfWork.Customers.GetCustomerByIdAsync(request.CustomerId);
            if (entity == null)
                return new OperationResult(OperationResultStatus.NotFound, value: CustomerErrors.CustomerNotFoundError);

            // Map
            var response = entity.MapToCustomerResponse();

            return new OperationResult(OperationResultStatus.Ok, value: response);
        }
    }
}