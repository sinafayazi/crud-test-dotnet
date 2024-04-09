using CustomerService.Application.Interfaces;
using CustomerService.Application.Interfaces.Repositories;
using CustomerService.Persistence.Repositories.Customers;

namespace CustomerService.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ICustomerRepository Customers { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Customers = new CustomerRepository(_context);
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}