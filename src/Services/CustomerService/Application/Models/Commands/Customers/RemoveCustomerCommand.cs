using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using MediatR;

namespace CustomerService.Application.Models.Commands.Customers
{
    public class RemoveCustomerCommand : Request, IRequest<OperationResult>
    {
        public int CustomerId { get; set; }
    }
}
