using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Dtos.ReportDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IReportService
    {
        Task<IDataResult> AddAsync(ReportAddDto reportAddDto);
        Task<IDataResult> UpdateAsync(ReportUpdateDto reportUpdateDto);
        Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult> GetByIdAsync(int id);
        Task<IDataResult> GetAllBySellerIdAsync(int sellerId);
        Task<IDataResult> GetAllByBrandIdAsync(int brandId);
        Task<IDataResult> GetAllByCustomerIdAsync(int customerId);
        Task<IDataResult> GetAllByProductIdAsync(int productId);
        Task<IDataResult> GetAllByCommentIdAsync(int commentId);
        Task<IDataResult> DeleteByIdAsync(int id);
        Task<IDataResult> HardDeleteByIdAsync(int id);
    }
}
