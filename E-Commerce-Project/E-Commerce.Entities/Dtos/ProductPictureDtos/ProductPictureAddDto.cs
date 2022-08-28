using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.ProductPictureDtos
{
    public class ProductPictureAddDto
    {
        public int ProductId { get; set; }
        public IFormFile File { get; set; }
    }
}
