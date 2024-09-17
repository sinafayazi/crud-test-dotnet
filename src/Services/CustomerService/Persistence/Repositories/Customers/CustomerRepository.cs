using Communal.Persistence.Extensions.Common;
using CustomerService.Application.Interfaces.Repositories;
using CustomerService.Application.Models.Responses.Customers;
using CustomerService.Domain.Customers;
using CustomerService.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Persistence.Repositories.Customers
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly IQueryable<Customer> _queryable;

        public CustomerRepository(AppDbContext context) : base(context)
        {
            _queryable = DbContext.Set<Customer>();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _queryable.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            return await _queryable.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<int> CountCustomersByFilterAsync(CustomerFilter filter)
        {
            var query = _queryable;

            query = query.ApplyFilter(filter);

            return await query.CountAsync();
        }

        public async Task<List<Customer>> GetCustomersByIdsAsync(IEnumerable<int> ids)
        {
            var query = _queryable.AsNoTracking();

            // Filter by ids
            if (ids?.Any() == true)
                query = query.Where(x => ids.Contains(x.Id));

            query = query.ApplySort(CustomerSortBy.CreationDate);

            return await query.ToListAsync();
        }

        public async Task<List<Customer>> GetCustomersByFilterAsync(CustomerFilter filter)
        {
            var query = _queryable;

            query = query.AsNoTracking();

            query = query.ApplyFilter(filter);
            query = query.ApplySort(filter.SortBy);

            return await query.Paginate(filter.Page, filter.PageSize).ToListAsync();
        }
    }
}