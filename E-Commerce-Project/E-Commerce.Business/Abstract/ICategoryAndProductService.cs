using E_Commerce.Entities.Dtos.CategoryAndProductDtos;
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
        Task<IDataResult> AddAsync(CategoryAndProductAddDto categoryAndProductAddDto);
        Task<IDataResult> UpdateAsync(CategoryAndProductUpdateDto categoryAndProductUpdateDto);
        Task<IDataResult> DeleteByCategoryIdAndProductIdAsync(int categoryId, int productId);
        Task<IDataResult> GetByProductIdAsync(int productId, bool includeProduct);
        Task<IDataResult> GetByCategoryIdAsync(int categoryId, bool includeCategory);
    }
}
