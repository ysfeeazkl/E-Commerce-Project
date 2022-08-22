using E_Commerce.Shared.Entities.ComplexTypes;
using E_Commerce.Shared.Utilities.Results.Abstract;

namespace E_Commerce.Shared.Utilities.Results.Concrete
{
    public class SuccessApiResult : ApiResult
    {
        public SuccessApiResult(IResult result, string href) : base(result.ResultStatus, result.Message, HttpStatusCode.OK, href)
        {
        }
    }
}

