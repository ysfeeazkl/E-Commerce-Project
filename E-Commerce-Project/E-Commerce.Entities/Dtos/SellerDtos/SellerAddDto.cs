using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.SellerDtos
{
    public class SellerAddDto
    {
        public string Name { get; set; }
        public IFormFile? File { get; set; }

    }
}
