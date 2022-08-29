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
        Task<IDataResult> GetByID(int id);
        Task<IDataResult> GetAllBySellerID(int sellerId);
        Task<IDataResult> GetAllByBrandID(int brandId);
        Task<IDataResult> GetAllByCustomerID(int customerId);
        Task<IDataResult> GetAllByProductID(int productId);
        Task<IDataResult> GetAllByCommentID(int commentId);
        Task<IDataResult> DeleteByIdAsync(int id);
        Task<IDataResult> HardDeleteByIdAsync(int id);
    }
}
