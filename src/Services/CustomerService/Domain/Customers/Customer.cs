
namespace CustomerService.Domain.Customers
{
    public partial class Customer : IEntity
    {
        #region Identifier
        public int Id { get; set; }

        #endregion
        
        #region Features


        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BankAccountNumber { get; set; }

        #endregion
        
        #region Mangement
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        #endregion

    }
}