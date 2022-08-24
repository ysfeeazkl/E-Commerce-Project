using E_Commerce.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class UserToken : IEntity
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddress { get; set; }
    }
}
