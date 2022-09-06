using E_Commerce.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class ShoppingCart :EntityBase<int> , IEntity
    {
        public Customer Customer { get; set; }
        public int CustomerID { get; set; }
       //  public decimal? TotalPrice { get { return Products.Sum(a => a.Price); } set { TotalPrice = value; }}
        public ICollection<Product>? Products { get; set; }

        public ShoppingCart()
        {
            Products = new List<Product>();
        }
    }
}
