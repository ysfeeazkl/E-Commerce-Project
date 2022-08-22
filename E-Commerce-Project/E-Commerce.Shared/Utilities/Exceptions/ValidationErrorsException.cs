using E_Commerce.Shared.Entities.Concrete;

namespace E_Commerce.Shared.Utilities.Exceptions
{
    public class ValidationErrorsException : Exception
    {
        public ValidationErrorsException(string message, IEnumerable<Error> validationErrors) : base(message)
        {
            ValidationErrors = validationErrors;
        }
        public IEnumerable<Error> ValidationErrors { get; set; }
    }
}

