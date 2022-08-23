using E_Commerce.Entities.Dtos.CustomerDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IAuthService
    {
        Task<IDataResult> RegisterAsync(CustomerRegisterDto customerRegisterDto);
        Task<IDataResult> LoginAsync(CustomerLoginDto customerRegisterDto);
    }
}
