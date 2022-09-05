using E_Commerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Concrete.Mappings
{
    public class ShoppingCartMap : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).ValueGeneratedOnAdd();

            //builder.HasOne<Customer>(a => a.Customer).WithOne(a => a.ShoppingCart).HasForeignKey<Customer>(c => c.ShoppingCartID);

            builder.HasMany<Product>(a => a.Products).WithOne(a => a.ShoppingCart).HasForeignKey(a => a.ID).OnDelete(DeleteBehavior.NoAction);


            builder.ToTable("ShoppingCarts");
        }
    }
}
