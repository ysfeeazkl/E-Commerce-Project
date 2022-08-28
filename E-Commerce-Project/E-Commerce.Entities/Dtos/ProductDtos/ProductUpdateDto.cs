using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.ProductDtos
{
    public class ProductUpdateDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Like { get; set; }
        public int SellerID { get; set; }
        public int? BrandID { get; set; }
    }
}
