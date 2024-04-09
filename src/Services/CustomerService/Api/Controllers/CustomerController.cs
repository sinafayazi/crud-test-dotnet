using Communal.Api.Extensions.AspNetCore;
using CustomerService.Api.Models.Requests.Customers;
using CustomerService.Api.ResultFilters.Customers;
using CustomerService.Application.Models.Commands.Customers;
using CustomerService.Application.Models.Queries.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Api.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Create
        [HttpPost(Routes.Customers)]
        [CreateCustomerResultFilter]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            // Operation
            var operation = await _mediator.Send(new CreateCustomerCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BankAccountNumber = request.BankAccountNumber,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email
            });

            return this.ReturnResponse(operation);
        }

        // Detail
        [HttpGet(Routes.Customers + "{id}")]
        [GetCustomerResultFilter]
        public async Task<IActionResult> GetCustomerDetail([FromRoute] int id)
        {

            // Operation
            var operation = await _mediator.Send(new GetCustomerQuery
            {
                CustomerId = id
            });

            return this.ReturnResponse(operation);
        }

        [HttpPatch(Routes.Customers + "{id}")]
        [UpdateCustomerResultFilter]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] UpdateCustomerRequest request)
        {
            // Operation
            var operation = await _mediator.Send(new UpdateCustomerCommand
            {
                CustomerId = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BankAccountNumber = request.BankAccountNumber,
                PhoneNumber = request.PhoneNumber,
                DateOfBirth = request.DateOfBirth,
                Email = request.Email
            });

            return this.ReturnResponse(operation);
        }

        [HttpDelete(Routes.Customers + "{id}")]
        [UpdateCustomerResultFilter]
        public async Task<IActionResult> RemoveCustomer([FromRoute] int id)
        {
            // Operation
            var operation = await _mediator.Send(new RemoveCustomerCommand
            {
                CustomerId = id,
            });

            return this.ReturnResponse(operation);
        }
        
    }
}