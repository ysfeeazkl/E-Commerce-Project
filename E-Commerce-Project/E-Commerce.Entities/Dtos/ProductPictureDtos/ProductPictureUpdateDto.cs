using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.ProductPictureDtos
{
    public class ProductPictureUpdateDto
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public IFormFile File { get; set; }
    }
}
