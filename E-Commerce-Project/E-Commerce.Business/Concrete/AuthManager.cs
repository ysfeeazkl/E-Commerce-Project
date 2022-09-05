using AutoMapper;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.AbstractUtilities;
using E_Commerce.Business.Utilities;
using E_Commerce.Business.ValidationRules.FluentValidation.AuthValidator;
using E_Commerce.Data.Concrete.Context;
using E_Commerce.Entities.Concrete;
using E_Commerce.Entities.Dtos.AuthDtos;
using E_Commerce.Entities.Dtos.CustomerDtos;
using E_Commerce.Entities.Dtos.UserTokenDtos;
using E_Commerce.Shared.Utilities.Hashing;
using E_Commerce.Shared.Utilities.Results.Abstract;
using E_Commerce.Shared.Utilities.Results.ComplexTypes;
using E_Commerce.Shared.Utilities.Results.Concrete;
using E_Commerce.Shared.Utilities.Security.Jwt;
using E_Commerce.Shared.Utilities.Validation.FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Concrete
{
    public class AuthManager : ManagerBase, IAuthService
    {
        IJwtHelper _jwtHelper;
        IHttpContextAccessor _httpContextAccessor;

        public AuthManager(CommerceContext context, IMapper mapper, IJwtHelper jwtHelper, IHttpContextAccessor httpContextAccessor) : base(mapper, context)
        {
            _jwtHelper = jwtHelper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AccessToken> CreateAccessTokenAsync(Customer customer, bool isRefresh)
        {
            var claims = await GetClaimsAsync(customer);
            var accessToken = _jwtHelper.CreateToken(customer, claims, isRefresh);
            return accessToken;
        }

        public async Task<IDataResult> CreateAccessTokenByCustomerIdAsync(int customerId, bool isRefresh)
        {
            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.ID == customerId);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı.");
            var claims = await GetClaimsAsync(customer);
            var accessToken = _jwtHelper.CreateToken(customer, claims, isRefresh);
            return new DataResult(ResultStatus.Success, accessToken);
        }


        public async Task<IEnumerable<OperationClaim>> GetClaimsAsync(Customer customer)
        {
            var roles = await DbContext.OperationClaims.ToListAsync();
            var userRoles = DbContext.CustomerAndOperationClaims.Where(a => a.CustomerID == customer.ID);
            var list = new List<OperationClaim>();
            await userRoles.ForEachAsync(a =>
            {
                var role = roles.SingleOrDefault(b => b.ID == a.OperationClaimID);
                if (role != null) list.Add(role);
            });
            return list;
            
        }

       
        public async Task<IDataResult> LoginWithEmailAsync(CustomerLoginWithEmailDto customerLoginWithEmailDto)
        {
            ValidationTool.Validate(new CustomerLoginWithEmailDtoValidator(), customerLoginWithEmailDto);

            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.EmailAddress == customerLoginWithEmailDto.EmailAddress);
            if (customer is null)
                return new DataResult(ResultStatus.Error,"Böyle bir kullanıcı bulunamadı");

            if (HashingHelper.VerifyPasswordHash(customerLoginWithEmailDto.Password, customer.PasswordHash, customer.PasswordSalt))
            {
                if (!customer.IsActive)
                    return new DataResult(ResultStatus.Error, "Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.");

                customer.LastLogin = DateTime.Now;
                customer.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                var accessToken = await CreateAccessTokenAsync(customer, false);
                UserToken userToken = new UserToken
                {
                    CustomerID = customer.ID,
                    Token = accessToken.Token,
                    TokenExpiration = accessToken.TokenExpiration,
                    CreatedDate = DateTime.Now,
                    IpAddress = customer.IpAddress
                };
                DbContext.Customers.Update(customer);
                await DbContext.UserTokens.AddAsync(userToken);
                await DbContext.SaveChangesAsync();
                var userLoginDto = new UserWithTokenDto
                {
                    Customer = Mapper.Map<CustomerDto>(customer),
                    Token = Mapper.Map<UserTokenDto>(userToken),
                };

                return new DataResult(ResultStatus.Success, $"{userLoginDto.Customer.FirstName} Hoşgeldiniz", userLoginDto);
            }
            return new DataResult(ResultStatus.Error, "Lütfen bilgilerinizi kontrol ediniz.");
        }

        public async Task<IDataResult> LoginWithPhoneAsync(CustomerLoginWithPhoneDto customerLoginWithPhoneDto)
        {
            ValidationTool.Validate(new CustomerLoginWithPhoneDtoValidator(), customerLoginWithPhoneDto);

            var customer = await DbContext.Customers.SingleOrDefaultAsync(a => a.PhoneNumber == customerLoginWithPhoneDto.PhoneNumber);
            if (customer is null)
                return new DataResult(ResultStatus.Error, "Böyle bir kullanıcı bulunamadı");

            if (HashingHelper.VerifyPasswordHash(customerLoginWithPhoneDto.Password, customer.PasswordHash, customer.PasswordSalt))
            {
                if (!customer.IsActive)
                    return new DataResult(ResultStatus.Error, "Hesabınızı Aktif Etmek İçin Destek ile İletişime Geçiniz.");

                customer.LastLogin = DateTime.Now;
                customer.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

                var accessToken = await CreateAccessTokenAsync(customer, false);
                UserToken userToken = new UserToken
                {
                    CustomerID = customer.ID,
                    Token = accessToken.Token,
                    TokenExpiration = accessToken.TokenExpiration,
                    CreatedDate = DateTime.Now,
                    IpAddress = customer.IpAddress
                };
                DbContext.Customers.Update(customer);
                await DbContext.UserTokens.AddAsync(userToken);
                await DbContext.SaveChangesAsync();
                var userLoginDto = new UserWithTokenDto
                {
                    Customer = Mapper.Map<CustomerDto>(customer),
                    Token = Mapper.Map<UserTokenDto>(userToken),
                };

                return new DataResult(ResultStatus.Success,$"{userLoginDto.Customer.FirstName} Hoşgeldiniz", userLoginDto );
            }
            return new DataResult(ResultStatus.Error, "Lütfen bilgilerinizi kontrol ediniz.");
        }

        public async Task<IDataResult> RegisterAsync(CustomerRegisterDto customerRegisterDto)
        {
            ValidationTool.Validate(new RegisterDtoValidator(), customerRegisterDto);

            if (await DbContext.Customers.SingleOrDefaultAsync(a => a.PhoneNumber == customerRegisterDto.PhoneNumber || a.UserName == customerRegisterDto.UserName || a.EmailAddress == customerRegisterDto.EmailAddress) is not null)
                return new DataResult(ResultStatus.Error, "Bu kullanıcı mevcut");

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(customerRegisterDto.Password, out passwordHash, out passwordSalt);
            var customer = Mapper.Map<Customer>(customerRegisterDto);
            customer.Birthday = DateTime.Parse(customerRegisterDto.Birth, new CultureInfo("es-ES"));
            customer.UserName = customer.UserName.ToLower();
            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;
            customer.IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            customer.CreatedDate = DateTime.Now;
            customer.IsActive = true;
            var accessToken = await CreateAccessTokenAsync(customer, false);
            await DbContext.Customers.AddAsync(customer);
            await DbContext.SaveChangesAsync();

            UserToken userToken = new UserToken
            {
                CustomerID = customer.ID,
                Token = accessToken.Token,
                TokenExpiration = accessToken.TokenExpiration,
                CreatedDate = DateTime.Now,
                IpAddress = customer.IpAddress
            };
            await DbContext.UserTokens.AddAsync(userToken);
            CustomerAndOperationClaim customerOperationClaim = new CustomerAndOperationClaim
            {
                CustomerID = customer.ID,
                OperationClaimID = 4 //Customer
            };
            await DbContext.CustomerAndOperationClaims.AddAsync(customerOperationClaim);
            await DbContext.SaveChangesAsync();
            return new DataResult(ResultStatus.Success, $"Hoşgeldiniz Sayın {customer.FirstName} {customer.LastName}.", new UserWithTokenDto
            {
                Customer = Mapper.Map<CustomerDto>(customer),
                Token = Mapper.Map<UserTokenDto>(userToken),
            });
        }


        
    }
}
