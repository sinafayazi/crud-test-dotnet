using Communal.Application.Infrastructure.Pagination;

namespace CustomerService.Application.Models.Responses.Customers
{
    public class CustomerFilter : PaginationFilter
    {
        protected CustomerFilter(int page, int pageSize) : base(page, pageSize)
        {
        }

        public string Keyword { get; set; }
        public string Email { get; set; }
        
        public CustomerSortBy? SortBy { get; set; }
    }
}