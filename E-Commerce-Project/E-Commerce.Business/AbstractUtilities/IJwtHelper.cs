using E_Commerce.Entities.Concrete;
using E_Commerce.Shared.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Business.AbstractUtilities
{
    public interface IJwtHelper
    {
        AccessToken CreateToken(Customer customer, IEnumerable<OperationClaim> operationClaims, bool isRefreshToken);

    }
}
