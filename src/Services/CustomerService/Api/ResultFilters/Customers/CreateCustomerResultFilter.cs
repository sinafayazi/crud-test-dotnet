using CustomerService.Domain.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomerService.Api.ResultFilters.Customers
{
    public class CreateCustomerResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is Customer value)
                result.Value = new
                {
                    Id = value.Id,
                    Email = value.Email
                };

            await next();
        }
    }
}