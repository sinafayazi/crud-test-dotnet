using System.Linq.Expressions;
using CustomerService.Domain.Customers;

namespace CustomerService.Application.Specifications.Customers
{
    public class DuplicateCustomerByFeatureSpecification : Specification<Customer>
    {
        private readonly string _customerFirstName;
        private readonly string _customerLastName;
        private readonly DateTime _customerDateOfBirth;
        private readonly int? _customerId;

        public DuplicateCustomerByFeatureSpecification(string customerFirstName,
            string customerLastName,
            DateTime customerDateOfBirth, int? customerId = null)
        {
            _customerFirstName = customerFirstName;
            _customerLastName = customerLastName;
            _customerDateOfBirth = customerDateOfBirth;
            _customerId = customerId;
        }

        public override Expression<Func<Customer, bool>> ToExpression()
        {
            return customer => customer.FirstName.ToLower() == _customerFirstName.ToLower()
                && customer.LastName.ToLower() == _customerLastName.ToLower()
                && customer.DateOfBirth == _customerDateOfBirth
                && (!_customerId.HasValue || customer.Id != _customerId.Value);
        }
    }
}