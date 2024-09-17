using CustomerService.Application.Models.Responses.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomerService.Api.ResultFilters.Customers
{
    public class GetCustomerResultFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result?.Value is CustomerResponse value)
                result.Value = new
                {
                    value.Id,
                    value.FirstName,
                    value.LastName,
                    value.Fullname,
                    value.Email,
                    value.DateOfBirth,
                    value.BankAccountNumber,
                    value.PhoneNumber,
                    value.CreatedAt,
                    value.UpdatedAt
                };

            await next();
        }
    }
}