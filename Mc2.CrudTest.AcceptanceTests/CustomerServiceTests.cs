using Communal.Application.Infrastructure.Operations;
using CustomerService.Application.Handlers.Customers;
using CustomerService.Application.Interfaces;
using CustomerService.Application.Interfaces.Repositories;
using CustomerService.Application.Models.Commands.Customers;
using CustomerService.Domain.Customers;
using Moq;
using NUnit.Framework;
using NUnit;

namespace Mc2.CrudTest.AcceptanceTests;


    [Binding]
    public class CustomerServiceTests
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateCustomerCommandHandler _handler;
        private CreateCustomerCommand _command;
        private OperationResult _result;

        public CustomerServiceTests(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockRepository = new Mock<ICustomerRepository>();
            mockUnitOfWork.Setup(u => u.Customers).Returns(mockRepository.Object);
            _unitOfWork = mockUnitOfWork.Object;
            _handler = new CreateCustomerCommandHandler(_unitOfWork);
        }

        [Given(@"a customer with the following details:")]
        public void GivenACustomerWithTheFollowingDetails(Table table)
        {
            var row = table.Rows[0];
            _command = new CreateCustomerCommand
            {
                FirstName = row["FirstName"],
                LastName = row["LastName"],
                DateOfBirth = DateTime.Parse(row["DateOfBirth"]),
                PhoneNumber = row["PhoneNumber"],
                Email = row["Email"],
                BankAccountNumber = row["BankAccountNumber"]
            };
        }

        [When(@"the user adds the customer")]
        public async Task WhenTheUserAddsTheCustomer()
        {
            _result = await _handler.Handle(_command, CancellationToken.None);
        }

        [Then(@"the customer should be successfully added")]
        public void ThenTheCustomerShouldBeSuccessfullyAdded()
        {
            Assert.That(_result.Status, Is.EqualTo(OperationResultStatus.Created));
        }
}