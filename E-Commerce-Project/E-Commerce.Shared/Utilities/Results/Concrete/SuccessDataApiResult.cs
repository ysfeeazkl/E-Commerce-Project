using E_Commerce.Shared.Entities.ComplexTypes;
using E_Commerce.Shared.Utilities.Results.Abstract;

namespace E_Commerce.Shared.Utilities.Results.Concrete
{
    public class SuccessDataApiResult : ApiResult
    {
        public SuccessDataApiResult(IDataResult dataResult, string href) : base(dataResult.ResultStatus, dataResult.Message, HttpStatusCode.OK, href)
        {
            Data = dataResult.Data;
        }
        public Object Data { get; set; }
    }
}

