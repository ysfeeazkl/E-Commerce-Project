using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class OrderHistory
    {
        Customer Customer { get; set; }
        int CustomerID { get; set; }
        ShoppingCart ShoppingCart { get; set; }
        int ShoppingCartID { get; set; }

    }
}
