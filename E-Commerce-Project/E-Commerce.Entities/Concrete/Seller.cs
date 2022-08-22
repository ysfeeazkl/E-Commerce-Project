using E_Commerce.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class Seller:EntityBase<int>,IEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Comment> Comments{ get; set; }
    }
}
