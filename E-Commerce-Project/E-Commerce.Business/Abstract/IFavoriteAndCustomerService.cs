using E_Commerce.Entities.Dtos.FavoriteAndCustomerDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IFavoriteAndCustomerService
    {
        Task<IDataResult> AddAsync(FavoriteAndCustomerAddDto favoriteAndCustomerAddDto);
        Task<IDataResult> UpdateAsync(FavoriteAndCustomerUpdateDto favoriteAndCustomerUpdate);
        Task<IDataResult> DeleteByFavoriteIdAndCustomerIdAsync(int customerId, int productId);
        Task<IDataResult> GetByFavoriteIdAsync(int productId, bool includeProduct);
        Task<IDataResult> GetByCustomerIdAsync(int customerId, bool includeCustomer);
    }
}
