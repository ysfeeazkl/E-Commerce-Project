using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class CategoryAndProduct
    {
        public Category Category { get; set; }
        public int CategoryID { get; set; }
        public Product Product { get; set; }
        public int ProductID { get; set; }
    }
}
