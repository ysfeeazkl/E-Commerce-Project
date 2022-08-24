using E_Commerce.Entities.Dtos.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.UserTokenDtos
{
    public class UserWithTokenDto
    {
        public CustomerDto Customer{ get; set; }
        public UserTokenDto Token { get; set; }
    }
}
