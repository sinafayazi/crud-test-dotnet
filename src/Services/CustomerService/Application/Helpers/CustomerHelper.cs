using CustomerService.Application.Models.Commands.Customers;
using CustomerService.Domain.Customers;

namespace CustomerService.Application.Helpers
{
    public static class CustomerHelper
    {
        public static Customer CreateCustomer(CreateCustomerCommand command) => new Customer
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            BankAccountNumber = command.BankAccountNumber,
            DateOfBirth = command.DateOfBirth,
            PhoneNumber = command.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        public static string GetFullName(this Customer customer) => $"{customer.FirstName} {customer.LastName}";
    }
}