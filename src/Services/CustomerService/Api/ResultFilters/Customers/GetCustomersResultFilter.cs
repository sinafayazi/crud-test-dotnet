using Communal.Application.Infrastructure.Pagination;
using CustomerService.Application.Models.Responses.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomerService.Api.ResultFilters.Customers
{
    public class GetCustomersResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is PaginatedList<CustomerResponse> value)
                result.Value = new
                {
                    value.Page,
                    value.PageSize,
                    value.TotalCount,
                    Data = value.Data.Select(x => new
                    {
                        Id = x.Id,
                        Fullname = x.Fullname,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        UpdatedAt = x.UpdatedAt
                    })
                };

            await next();
        }
    }
}
