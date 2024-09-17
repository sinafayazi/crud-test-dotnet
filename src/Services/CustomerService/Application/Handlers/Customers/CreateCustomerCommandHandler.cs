using Communal.Application.Infrastructure.Operations;
using CustomerService.Application.Errors;
using CustomerService.Application.Helpers;
using CustomerService.Application.Interfaces;
using CustomerService.Application.Models.Commands.Customers;
using CustomerService.Application.Specifications.Customers;
using MediatR;

namespace CustomerService.Application.Handlers.Customers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            // Checking duplicate
            var isExist = await _unitOfWork.Customers
                .ExistsAsync(new DuplicateCustomerSpecification(request.Email).ToExpression());
            if (isExist)
                return new OperationResult(OperationResultStatus.Unprocessable, value: CustomerErrors.DuplicateEmailError);
            
            var isDuplicate = await _unitOfWork.Customers
                .ExistsAsync(new  DuplicateCustomerByFeatureSpecification(request.FirstName, request.LastName, request.DateOfBirth)
                    .ToExpression());
            if (isDuplicate)
                return new OperationResult(OperationResultStatus.Unprocessable, value: CustomerErrors.DuplicateCustomerError);
            
            // Factory
            var entity = CustomerHelper.CreateCustomer(request);

            _unitOfWork.Customers.Add(entity);

            return new OperationResult(OperationResultStatus.Created, value: entity, isPersistable: true);
        }
    }
}