using Communal.Application.Infrastructure.Operations;
using CustomerService.Application.Handlers.Customers;
using CustomerService.Application.Interfaces;
using CustomerService.Application.Interfaces.Repositories;
using CustomerService.Application.Models.Commands.Customers;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Communal.Application.Infrastructure.Pagination;
using CustomerService.Application.Behaviors.Common;
using CustomerService.Application.Behaviors.Customers;
using CustomerService.Application.Models.Queries.Customers;
using CustomerService.Application.Models.Responses.Customers;
using MediatR;
using TechTalk.SpecFlow;

namespace Tests.Steps
{
    [Binding]
    public class CustomerServiceTests
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateCustomerCommandHandler _handler;
        private readonly CreateCustomerValidationBehavior<CreateCustomerCommand, OperationResult> _validator;
        private List<CreateCustomerCommand> _commands;
        private List<OperationResult> _results;
        private RequestHandlerDelegate<OperationResult> _next = () => Task.FromResult(new OperationResult(OperationResultStatus.Ok, null));

        public CustomerServiceTests(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<ICustomerRepository>();
            mockUnitOfWork.Setup(u => u.Customers).Returns(mockRepository.Object);
            _unitOfWork = mockUnitOfWork.Object;
            _handler = new CreateCustomerCommandHandler(_unitOfWork);
            _validator = new CreateCustomerValidationBehavior<CreateCustomerCommand, OperationResult>();
        }

        [Given(@"a customer with the following details:")]
        public void GivenACustomerWithTheFollowingDetails(Table table)
        {
            _commands = new List<CreateCustomerCommand>();
            foreach (var row in table.Rows)
            {
                var command = new CreateCustomerCommand
                {
                    FirstName = row["FirstName"],
                    LastName = row["LastName"],
                    DateOfBirth = DateTime.Parse(row["DateOfBirth"]),
                    PhoneNumber = row["PhoneNumber"],
                    Email = row["Email"],
                    BankAccountNumber = row["BankAccountNumber"]
                };
                _commands.Add(command);
            }
        }

        [When(@"the user adds the customer")]
        public async Task WhenTheUserAddsTheCustomer()
        {
            _results = new List<OperationResult>();
            foreach (var command in _commands)
            {
                var result = await _handler.Handle(command, CancellationToken.None);
                _results.Add(result);
            }
        }

        [Then(@"the customer should be successfully added")]
        public void ThenTheCustomerShouldBeSuccessfullyAdded()
        {
            foreach (var result in _results)
            {
                Assert.That(result.Status, Is.EqualTo(OperationResultStatus.Created));
            }
        }

        [When(@"the system validates the customer")]
        public async Task WhenTheSystemValidatesTheCustomer()
        {
            _results = new List<OperationResult>();
            foreach (var command in _commands)
            {
                var result = await _validator.Handle(command, CancellationToken.None, _next  );
                _results.Add(result);
            }
        }

        [Then(@"the customer validation should be successful")]
        public void ThenTheCustomerValidationShouldBeSuccessful()
        {
            foreach (var result in _results)
            {
                Assert.That(result.Status, Is.EqualTo(OperationResultStatus.Ok));
            }
        }

        [Given(@"a customer with the following invalid details:")]
        public void GivenACustomerWithTheFollowingInvalidDetails(Table table)
        {
            _commands = new List<CreateCustomerCommand>();
            foreach (var row in table.Rows)
            {
                var command = new CreateCustomerCommand
                {
                    FirstName = row["FirstName"],
                    LastName = row["LastName"],
                    DateOfBirth = DateTime.Parse(row["DateOfBirth"]),
                    PhoneNumber = row["PhoneNumber"],
                    Email = row["Email"],
                    BankAccountNumber = row["BankAccountNumber"]
                };
                _commands.Add(command);
            }
        }

        [Then(@"the customer validation should fail")]
        public void ThenTheCustomerValidationShouldFail()
        {
            foreach (var result in _results)
            {
                Assert.That(result.Status, Is.EqualTo(OperationResultStatus.InvalidRequest));
            }
        }

        [Then(@"the system should return an error message")]
        public void ThenTheSystemShouldReturnAnErrorMessage()
        {
            foreach (var result in _results)
            {
                Assert.That(result.Value, Is.Not.Null.Or.Empty);
            }
        }
    }
}