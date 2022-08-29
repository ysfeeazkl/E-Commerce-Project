using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.SellerPictureDtos
{
    public class SellerPictureUpdateDto
    {
        public int ID { get; set; }
        public int SellerID { get; set; }
        public IFormFile File { get; set; }
    }
}
