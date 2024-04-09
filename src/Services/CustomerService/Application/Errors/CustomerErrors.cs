using Communal.Application.Constants;
using Communal.Application.Infrastructure.Errors;

namespace CustomerService.Application.Errors
{
    public static class CustomerErrors
    {
        // Code ranges for Customer Service is between 00001 and 00099
        public static ErrorModel CustomerNotFoundError = new ErrorModel(
            code: 00001,
            title: "Customer Service Error",
            (
                Language: Language.English,
                Message: "Customer not found!!!"
            ));

        public static ErrorModel DuplicateEmailError = new ErrorModel(
            code: 00002,
            title: "Customer Service Error",
            (
                Language: Language.English,
                Message: "Email is duplicate!!!"
            ));
        
        public static ErrorModel DuplicateCustomerError = new ErrorModel(
            code: 00003,
            title: "Customer Service Error",
            (
                Language: Language.English,
                Message: "Customer is duplicate!!!"
            ));
    }
}