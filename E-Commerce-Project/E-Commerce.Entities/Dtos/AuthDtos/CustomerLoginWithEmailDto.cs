using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.AuthDtos
{
    public class CustomerLoginWithEmailDto
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
