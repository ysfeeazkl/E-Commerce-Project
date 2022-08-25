using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface ICategoryAndProductService
    {
        Task<IDataResult> AddAsync();
        Task<IDataResult> UpdateAsync();
        Task<IDataResult> DeleteByCategoryIdAndProductIdAsync(int categoryId, int productId);
        Task<IDataResult> GetByProductIdAsync(int productId, bool includeProduct);
        Task<IDataResult> GetByCategoryIdAsync(int categoryId, bool includeCategory);
    }
}
