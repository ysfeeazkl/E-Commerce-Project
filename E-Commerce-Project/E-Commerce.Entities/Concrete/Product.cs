using E_Commerce.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Entities.Concrete
{
    public class Product : EntityBase<int>, IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Like { get; set; }
        public Seller Seller { get; set; }
        public int SellerID { get; set; }
        public Brand? Brand { get; set; }
        public int BrandID { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int ShoppingCartID { get; set; }
        public ICollection<ProductPicture> ProductPictures { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CategoryAndProduct> CategoryAndProducts { get; set; }
        public ICollection<FavoriteAndCustomer> Favorites { get; set; }

    }
}
