using CustomerService.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Persistence
{
    public sealed class AppDbContext : DbContext
    {
        #region DbSets

        public DbSet<Customer> Customers { get; set; }

        #endregion

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            // Creating Model
            base.OnModelCreating(modelBuilder);
        }
    }
}