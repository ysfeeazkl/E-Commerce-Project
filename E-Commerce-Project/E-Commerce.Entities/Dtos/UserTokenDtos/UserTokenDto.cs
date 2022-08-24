using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.UserTokenDtos
{
    public class UserTokenDto
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddress { get; set; }
    }
}
