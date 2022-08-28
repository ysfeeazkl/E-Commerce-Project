using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.CustomerDtos
{
    public class CustomerUpdateDto
    {
        public int ID{ get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? UserName { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? LastLogin { get; set; }      
    }
}
