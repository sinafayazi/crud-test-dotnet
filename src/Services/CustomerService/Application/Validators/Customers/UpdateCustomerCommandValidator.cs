using CustomerService.Application.Models.Commands.Customers;
using CustomerService.Domain;
using FluentValidation;

namespace CustomerService.Application.Validators.Customers
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            // Customer id
            RuleFor(x => x.CustomerId)
                .Must(x => 0 < x)
                .WithState(_ => Errors.Errors.InvalidInputValidationError);

            // Firstname
            RuleFor(x => x.FirstName)
                .Length(2, Defaults.NameLength)
                .When(x => !string.IsNullOrEmpty(x.FirstName))
                .WithState(_ => Errors.Errors.InvalidFirstNameValidationError);

            // Lastname
            RuleFor(x => x.LastName)
                .Length(2, Defaults.NameLength)
                .When(x => !string.IsNullOrEmpty(x.LastName))
                .WithState(_ => Errors.Errors.InvalidLastNameValidationError);

            // Email
            RuleFor(x => x.Email)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
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
