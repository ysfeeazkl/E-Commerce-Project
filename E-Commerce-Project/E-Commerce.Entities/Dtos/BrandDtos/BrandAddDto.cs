using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.BrandDtos
{
    public class BrandAddDto
    {
        public string Name { get; set; }
        public IFormFile? File { get; set; }
        public string Introduc { get; set; }
    }
}
