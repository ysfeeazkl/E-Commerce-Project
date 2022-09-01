using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Dtos.CommentDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface ICommentService
    {
        Task<IDataResult> AddAsync(CommentAddDto commentAddDto);
        Task<IDataResult> UpdateAsync(CommentUpdateDto commentUpdateDto);
        Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderBy);
        Task<IDataResult> GetByIdAsync(int id);
        Task<IDataResult> GetAllByCustomerIdAsync(int customerId);
        Task<IDataResult> GetAllByProductIdAsync(int productId);
        Task<IDataResult> GetAllBySellerIdAsync(int sellerId);
        Task<IDataResult> GetAllByBaseCommentIdAsync(int baseCommentId);
        Task<IDataResult> DeleteByIdAsync(int id);
        Task<IDataResult> HardDeleteByIdAsync(int id);
    }
}
