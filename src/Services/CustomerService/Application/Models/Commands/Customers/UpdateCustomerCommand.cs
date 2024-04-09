using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using MediatR;

namespace CustomerService.Application.Models.Commands.Customers
{
    public class UpdateCustomerCommand : Request, IRequest<OperationResult>
    {
        public int CustomerId { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BankAccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
