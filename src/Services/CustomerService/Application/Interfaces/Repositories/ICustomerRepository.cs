using CustomerService.Application.Models.Responses.Customers;
using CustomerService.Domain.Customers;

namespace CustomerService.Application.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<int> CountCustomersByFilterAsync(CustomerFilter filter);
        Task<List<Customer>> GetCustomersByIdsAsync(IEnumerable<int> ids);
        Task<List<Customer>> GetCustomersByFilterAsync(CustomerFilter filter);
    }
}