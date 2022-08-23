using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Dtos.CustomerDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        public Task<IDataResult> LoginAsync(CustomerLoginDto customerRegisterDto)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult> RegisterAsync(CustomerRegisterDto customerRegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
