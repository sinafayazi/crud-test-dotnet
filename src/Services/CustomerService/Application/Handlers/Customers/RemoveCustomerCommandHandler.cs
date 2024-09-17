using Communal.Application.Infrastructure.Operations;
using CustomerService.Application.Errors;
using CustomerService.Application.Interfaces;
using CustomerService.Application.Models.Commands.Customers;
using MediatR;

namespace CustomerService.Application.Handlers.Customers
{
    public class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            // Get
            var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(request.CustomerId);
            if (customer == null)
                return new OperationResult(OperationResultStatus.Unprocessable, value: CustomerErrors.CustomerNotFoundError);

           
            _unitOfWork.Customers.Update(customer);

            return new OperationResult(OperationResultStatus.Ok, value: customer, isPersistable: true);
        }
    }
}
