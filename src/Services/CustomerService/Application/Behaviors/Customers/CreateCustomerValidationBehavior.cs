using Communal.Application.Infrastructure.Operations;
using CustomerService.Application.Helpers;
using CustomerService.Application.Models.Commands.Customers;
using CustomerService.Application.Validators.Customers;
using IbanNet;
using IbanNet.FluentValidation;
using MediatR;

namespace CustomerService.Application.Behaviors.Customers
{
    public class CreateCustomerValidationBehavior<TRequest, TResponse> : IPipelineBehavior<CreateCustomerCommand, OperationResult>
    {
        public async Task<OperationResult> Handle(CreateCustomerCommand request,
            CancellationToken cancellationToken, RequestHandlerDelegate<OperationResult> next)
        {
            // Validation
            var validator = new CreateCustomerCommandValidator();
            var validation = validator.Validate(request);
            var phoneValidation = validator.ValidatePhone(request.PhoneNumber);
            
            if (!validation.IsValid)
                return new OperationResult(OperationResultStatus.InvalidRequest, validation.GetFirstErrorMessage());
           // Phone 
           if (!phoneValidation)
               return new OperationResult(OperationResultStatus.InvalidRequest, Errors.Errors.InvalidPhoneNumberValidationError);
           
           //Iban
           IIbanValidator ibanValidator = new IbanValidator();
           ValidationResult validationResult = ibanValidator.Validate(request.BankAccountNumber);
           if (!validationResult.IsValid)
               return new OperationResult(OperationResultStatus.InvalidRequest, Errors.Errors.InvalidBankAccountNumberValidationError);
            
           return await next();
        }
    }
}