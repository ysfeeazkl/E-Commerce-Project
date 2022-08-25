﻿using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IReportService
    {
        Task<IDataResult> AddAsync();
        Task<IDataResult> UpdateAsync();
        Task<IDataResult> GetAllAsync();
        Task<IDataResult> GetByID(int id);
        Task<IDataResult> GetBySellerID(int sellerId);
        Task<IDataResult> GetByBrandID(int brandId);
        Task<IDataResult> GetByCustomerID(int customerId);
        Task<IDataResult> GetByProductID(int productId);
        Task<IDataResult> GetByCommentID(int commentId);
        Task<IDataResult> DeleteByIdAsync(int id);
        Task<IDataResult> HardDeleteByIdAsync(int id);
    }
}
