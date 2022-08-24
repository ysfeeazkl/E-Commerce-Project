using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.AuthDtos;
using E_Commerce.Entities.Dtos.CustomerDtos;
using E_Commerce.Shared.Utilities.Results.Abstract;
using E_Commerce.Shared.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Abstract
{
    public interface IAuthService
    {
        Task<IEnumerable<OperationClaim>> GetClaimsAsync(Customer customer);
        Task<AccessToken> CreateAccessTokenAsync(Customer customer, bool isRefresh);
        Task<IDataResult> CreateAccessTokenByCustomerIdAsync(int customerId, bool isRefresh);

        Task<IDataResult> RegisterAsync(CustomerRegisterDto customerRegisterDto);
        Task<IDataResult> LoginWithPhoneAsync(CustomerLoginWithPhoneDto customerLoginWithPhoneDto);
        Task<IDataResult> LoginWithEmailAsync(CustomerLoginWithEmailDto customerLoginWithEmailDto);

    }
}
