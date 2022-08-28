using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.ProductDtos
{
    public class ProductAddDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int SellerID { get; set; }
        public string? BrandName { get; set; }
        public IFormFile? File { get; set; }
    }
}
