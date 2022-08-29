using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.SellerPictureDtos
{
    public class SellerPictureAddDto
    {
        public int SellerID { get; set; }
        public IFormFile File { get; set; }

    }
}
