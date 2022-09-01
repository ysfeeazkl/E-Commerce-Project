using E_Commerce.Entities.Dtos.ShoppingCartDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IShoppingCartService
    {
        Task<IDataResult> AddAsync(ShoppingCartAddDto shoppingCartAddDto);
        Task<IDataResult> UpdateAsync(ShoppingCartUpdateDto shoppingCartUpdateDto);
        Task<IDataResult> GetAllAsync();
        Task<IDataResult> GetByIdAsync(int id);
        Task<IDataResult> GetByCustomerIdAsync(int customerId);
        Task<IDataResult> DeleteByIdAsync(int id);
        Task<IDataResult> HardDeleteByIdAsync(int id);
    }
}


