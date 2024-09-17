using Communal.Application.Infrastructure.Operations;
using Communal.Application.Models;
using CustomerService.Application.Models.Responses.Customers;
using MediatR;

namespace CustomerService.Application.Models.Queries.Customers
{
    public class GetCustomersQuery : Request, IRequest<OperationResult>
    {
        public CustomerFilter Filter { get; set; }
    }
}