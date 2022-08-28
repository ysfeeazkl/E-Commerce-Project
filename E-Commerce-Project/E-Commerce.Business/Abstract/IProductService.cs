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
        Task<IDataResult> LikeEventById (int id);
        Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult> GetByID(int id);
        Task<IDataResult> GetAllBySellerID(int sellerId);
        Task<IDataResult> GetAllByCategoryID(int categoryId);
        Task<IDataResult> GetAllByBrandID(int brandId);
        Task<IDataResult> DeleteByIdAsync(int id);
        Task<IDataResult> HardDeleteByIdAsync(int id);
    }
}
