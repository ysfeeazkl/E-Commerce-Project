using E_Commerce.Shared.Utilities.Results.Abstract;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;

namespace E_Commerce.Shared.Utilities.Results.Concrete
{
    public class Result : IResult
    {
        public Result(ResultStatus resultStatus, string message) : this(message)
        {
            ResultStatus = resultStatus;
        }
        public Result(string message)
        {
            Message = message;
        }
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
    }
}
