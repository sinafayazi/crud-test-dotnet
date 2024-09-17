using FluentValidation.Results;

namespace CustomerService.Application.Helpers
{
    public static class ValidationHelper
    {
        public static string GetFirstErrorMessage(this ValidationResult result)
        {
            return result.Errors.FirstOrDefault()?.ErrorMessage;
        }
    }
}
