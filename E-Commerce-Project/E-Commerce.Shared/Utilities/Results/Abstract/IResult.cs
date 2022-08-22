using E_Commerce.Shared.Utilities.Results.ComplexTypes;

namespace E_Commerce.Shared.Utilities.Results.Abstract
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get; }// ResultStatus.Success // ResultStatus.Error
        public string Message { get; }
    }
}
