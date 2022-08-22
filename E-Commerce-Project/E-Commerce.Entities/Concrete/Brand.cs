using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class Brand
    {
        public string Name { get; set; }
        public BrandPicture BrandPicture { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
