using E_Commerce.Business.AbstractUtilities;
using E_Commerce.Entities.Concrete;
using E_Commerce.Shared.Extensions;
using E_Commerce.Shared.Utilities.Security.Encryption;
using E_Commerce.Shared.Utilities.Security.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.Utilities
{
    public class JwtHelper : IJwtHelper
    {
        IConfiguration Configuration { get; }
        TokenOptions _tokenOptions;
        DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(Customer customer, IEnumerable<OperationClaim> operationClaims, bool isRefreshToken)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration + 300);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(customer, signingCredentials, operationClaims, isRefreshToken);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            return new AccessToken()
            {
                Token = token,
                TokenExpiration = _accessTokenExpiration,
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(Customer customer, SigningCredentials signingCredentials, IEnumerable<OperationClaim> operationClaims, bool refreshToken)
        {
            var jwt = new JwtSecurityToken
            (
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                expires: refreshToken == false ? _accessTokenExpiration : DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration + 100),
                notBefore: DateTime.Now,
                claims: SetClaims(customer, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }
        public IEnumerable<Claim> SetClaims(Customer customer, IEnumerable<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(customer.ID.ToString());
            claims.AddEmail(customer.EmailAddress);
            claims.AddName(customer.UserName);
            claims.AddPhone(customer.PhoneNumber);
            claims.AddIpAddress(customer.IpAddress!);
            claims.AddIsDeletedStatus(customer.IsDeleted);
            claims.AddRoles(operationClaims.Select(a => a.Name).ToArray());
            return claims;
        }
    }
}
