namespace CustomerService.Api.Models.Requests.Customers
{
    public class UpdateCustomerRequest
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BankAccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
