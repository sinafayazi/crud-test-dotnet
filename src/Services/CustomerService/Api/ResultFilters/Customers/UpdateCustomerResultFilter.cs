using CustomerService.Domain.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomerService.Api.ResultFilters.Customers
{
    public class UpdateCustomerResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is Customer value)
                result.Value = new
                {
                    Id = value.Id,
                    Email = value.Email,
                    UpdatedAt = value.UpdatedAt
                };

            await next();
        }
    }
}
