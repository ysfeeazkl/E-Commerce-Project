using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.CategoryAndProductDtos
{
    public class CategoryAndProductUpdateDto
    {
        public int CategoryID { get; set; }
        public int ProductID { get; set; }
        public int? NewCategoryID { get; set; }
        public int? NewProductID { get; set; }
    }
}
