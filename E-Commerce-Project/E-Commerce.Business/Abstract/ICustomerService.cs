using E_Commerce.Entities.ComplexTypes;
using E_Commerce.Entities.Dtos.CustomerDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface ICustomerService
    {
        Task<IDataResult> UpdateAsync(CustomerUpdateDto customerUpdateDto);
        Task<IDataResult> GetAllAsync(bool? isDeleted, bool isAscending, int currentPage, int pageSize, OrderBy orderByss);
        Task<IDataResult> GetByIdAsync(int id);
        Task<IDataResult> GetByUserNameAsync(string userName);
        Task<IDataResult> GetByEmailAddressAsync(string emailAddress);
        Task<IDataResult> GetByPhoneNumberAsync(string phoneNumber);
        Task<IDataResult> DeleteByIdAsync(int id);
        Task<IDataResult> HardDeleteByIdAsync(int id);

    }
}
