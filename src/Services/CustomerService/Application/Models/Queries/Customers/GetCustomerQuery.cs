using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using MediatR;

namespace CustomerService.Application.Models.Queries.Customers
{
    public class GetCustomerQuery : Request, IRequest<OperationResult>
    {
        public int CustomerId { get; set; }
    }
}