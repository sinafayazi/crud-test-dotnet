using CustomerService.Application.Interfaces.Repositories;

namespace CustomerService.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }

        Task<bool> CommitAsync();
    }
}