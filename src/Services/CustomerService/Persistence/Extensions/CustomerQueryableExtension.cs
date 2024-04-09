using CustomerService.Application.Extensions;
using CustomerService.Application.Models.Responses.Customers;
using CustomerService.Domain.Customers;

namespace CustomerService.Persistence.Extensions
{
    public static class CustomerQueryableExtension
    {
        public static IQueryable<Customer> ApplyFilter(this IQueryable<Customer> query, CustomerFilter filter)
        {
            // Filter by keyword
            if (!filter.Keyword.IsNullOrEmpty())
                query = query.Where(x =>
                    x.FirstName.ToLower().Contains(filter.Keyword.ToLower().Trim()) ||
                    x.LastName.ToLower().Contains(filter.Keyword.ToLower().Trim()));

            // Filter by email
            if (!filter.Email.IsNullOrEmpty())
                query = query.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower().Trim()));

            return query;
        }

        public static IQueryable<Customer> ApplySort(this IQueryable<Customer> query, CustomerSortBy? sortBy)
        {
            return sortBy switch
            {
                CustomerSortBy.CreationDate => query.OrderBy(x => x.CreatedAt),
                CustomerSortBy.CreationDateDescending => query.OrderByDescending(x => x.CreatedAt),
                CustomerSortBy.UpdateDate => query.OrderBy(x => x.UpdatedAt),
                CustomerSortBy.UpdateDateDescending => query.OrderByDescending(x => x.UpdatedAt),
                _ => query.OrderByDescending(x => x.Id)
            };
        }
    }
}