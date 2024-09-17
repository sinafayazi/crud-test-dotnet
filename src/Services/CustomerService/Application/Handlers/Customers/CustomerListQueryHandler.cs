using Communal.Application.Infrastructure.Operations;
using Communal.Application.Infrastructure.Pagination;
using CustomerService.Application.Interfaces;
using CustomerService.Application.Mappers;
using CustomerService.Application.Models.Queries.Customers;
using CustomerService.Application.Models.Responses.Customers;
using MediatR;

namespace CustomerService.Application.Handlers.Customers
{
    public class CustomerListQueryHandler : IRequestHandler<GetCustomersQuery, OperationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            // Get

            var entities = await _unitOfWork.Customers.GetCustomersByFilterAsync(request.Filter);
            var count = await _unitOfWork.Customers.CountCustomersByFilterAsync(request.Filter);

            // Mapper
            var response = entities.MapToCustomerResponses();

            var result = new PaginatedList<CustomerResponse>
            {
                Page = request.Filter.Page,
                PageSize = request.Filter.PageSize,
                Data = response.ToList(),
                TotalCount = count
            };

            return new OperationResult(OperationResultStatus.Ok, value: result);
        }
    }
}