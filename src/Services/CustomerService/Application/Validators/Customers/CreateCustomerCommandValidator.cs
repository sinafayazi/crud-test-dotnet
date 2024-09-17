using CustomerService.Application.Models.Commands.Customers;
using CustomerService.Domain;
using FluentValidation;

namespace CustomerService.Application.Validators.Customers
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            // Firstname
            RuleFor(x => x.FirstName)
                .MaximumLength(Defaults.NameLength)
                .WithState(_ => Errors.Errors.InvalidFirstNameValidationError);

            // Lastname
            RuleFor(x => x.LastName)
                .MaximumLength(Defaults.NameLength)
                .WithState(_ => Errors.Errors.InvalidLastNameValidationError);

            // Email
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithState(_ => Errors.Errors.InvalidEmailValidationError);
        }
        
        public bool ValidatePhone(string phoneNumber)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            try
            {
                var number = phoneNumberUtil.Parse(phoneNumber, null);
                return phoneNumberUtil.IsValidNumber(number);
            }
            catch
            {
                return false;
            }
        }
    }
}
