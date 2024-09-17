using Communal.Application.Infrastructure.Operations;
using CustomerService.Application.Errors;
using CustomerService.Application.Interfaces;
using CustomerService.Application.Models.Commands.Customers;
using CustomerService.Application.Specifications.Customers;
using MediatR;

namespace CustomerService.Application.Handlers.Customers
{
    internal class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            // Get
            var customer = await _unitOfWork.Customers.GetCustomerByIdAsync(request.CustomerId);
            if (customer == null)
                return new OperationResult(OperationResultStatus.Unprocessable, value: CustomerErrors.CustomerNotFoundError);

            var isExist = await _unitOfWork.Customers
                .ExistsAsync(new DuplicateCustomerSpecification(request.Email, customer.Id).ToExpression());
            if (isExist)
                return new OperationResult(OperationResultStatus.Unprocessable, value: CustomerErrors.DuplicateEmailError);
            
            var isDuplicate = await _unitOfWork.Customers
                .ExistsAsync(new DuplicateCustomerByFeatureSpecification(request.FirstName,
                        request.LastName,
                        request.DateOfBirth,
                        customer.Id)
                    .ToExpression());
            if (isDuplicate)
                return new OperationResult(OperationResultStatus.Unprocessable, value: CustomerErrors.DuplicateCustomerError);

            // Update
            customer.Email = request.Email;
            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.PhoneNumber = request.PhoneNumber;
            customer.DateOfBirth = request.DateOfBirth;
            customer.BankAccountNumber = request.BankAccountNumber;
            

            _unitOfWork.Customers.Update(customer);

            return new OperationResult(OperationResultStatus.Ok, value: customer, isPersistable: true);
        }
    }
}
