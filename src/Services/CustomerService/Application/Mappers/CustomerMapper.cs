using CustomerService.Application.Helpers;
using CustomerService.Application.Models.Responses.Customers;
using CustomerService.Domain.Customers;

namespace CustomerService.Application.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerResponse MapToCustomerResponse(this Customer customer)
        {
            return new CustomerResponse
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Fullname = customer.GetFullName(),
                DateOfBirth = customer.DateOfBirth,
                BankAccountNumber = customer.BankAccountNumber,
                PhoneNumber = customer.PhoneNumber,

                CreatedAt = customer.CreatedAt,
                UpdatedAt = customer.UpdatedAt,
            };
        }

        public static IEnumerable<CustomerResponse> MapToCustomerResponses(this IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
                yield return customer.MapToCustomerResponse();
        }
    }
}