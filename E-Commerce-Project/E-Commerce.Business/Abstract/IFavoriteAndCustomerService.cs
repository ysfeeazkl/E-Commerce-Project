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
        Task<IDataResult> AddAsync();
        Task<IDataResult> UpdateAsync();
        Task<IDataResult> DeleteByFavoriteIdAndCustomerIdAsync(int categoryId, int productId);
        Task<IDataResult> GetByFavoriteIdAsync(int favoriteId, bool includeFavorite);
        Task<IDataResult> GetByCustomerIdAsync(int customerId, bool includeCustomer);
    }
}
