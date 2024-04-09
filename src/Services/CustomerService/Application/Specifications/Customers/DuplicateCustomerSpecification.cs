using System.Linq.Expressions;
using CustomerService.Domain.Customers;

namespace CustomerService.Application.Specifications.Customers
{
    public class DuplicateCustomerSpecification : Specification<Customer>
    {
        private readonly string _email;
        private readonly int? _customerId;
        

        public DuplicateCustomerSpecification(string email, int? customerId = null)
        {
            _email = email;
            _customerId = customerId;
        }

        public override Expression<Func<Customer, bool>> ToExpression()
        {
            return customer => customer.Email.ToLower() == _email.ToLower()
                && (!_customerId.HasValue || customer.Id != _customerId.Value);
        }
    }
}