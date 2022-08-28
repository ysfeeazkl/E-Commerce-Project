using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Dtos.FavoriteAndCustomerDtos
{
    public class FavoriteAndCustomerUpdateDto
    {
        
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int? NewCustomerID { get; set; }
        public int? NewProductID { get; set; }
    }
}
