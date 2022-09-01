using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Dtos.ProductDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IProductService
    {
        Task<IDataResult> AddAsync(ProductAddDto productAddDto);
        Task<IDataResult> UpdateAsync(ProductUpdateDto productUpdateDto);
        Task<IDataResult> LikeEventByIdAsync(int id, int likeAndDislike);
        Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult> GetByIdAsync(int id);
        Task<IDataResult> GetAllBySellerIdAsync(int sellerId);
        Task<IDataResult> GetAllByCategoryIdAsync(int categoryId);
        Task<IDataResult> GetAllByBrandIdAsync(int brandId);
        Task<IDataResult> DeleteByIdAsync(int id);
        Task<IDataResult> HardDeleteByIdAsync(int id);
    }
}
