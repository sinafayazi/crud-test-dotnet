
namespace CustomerService.Application.Models.Responses.Customers
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
        public DateTime DateOfBirth { get; set; }
        public string BankAccountNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
        

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}