using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.ReportDtos
{
    public class ReportAddDto
    {
        public string Content { get; set; }
        public int CustomerID { get; set; }       
        public int? CommentID { get; set; }       
        public int? ProductID { get; set; }        
        public int? SellerID { get; set; }
        public int? BrandID { get; set; }
        public IFormFile? File { get; set; }

    }
}
