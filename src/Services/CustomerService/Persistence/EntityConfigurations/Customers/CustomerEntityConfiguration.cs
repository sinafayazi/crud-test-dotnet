using CustomerService.Domain;
using CustomerService.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerService.Persistence.EntityConfigurations.Customers
{
    internal class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(b => b.Email).IsUnique();
            
            builder.HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth })
                .IsUnique();

            #region Limmits

            builder.Property(b => b.Email)
                .HasMaxLength(Defaults.EmailLength);

            builder.Property(b => b.FirstName)
                .HasMaxLength(Defaults.NameLength);

            builder.Property(b => b.LastName)
                .HasMaxLength(Defaults.NameLength);
    

            #endregion
        }
    }
}