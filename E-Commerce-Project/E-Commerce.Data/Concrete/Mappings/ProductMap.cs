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
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).ValueGeneratedOnAdd();

            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Name).HasMaxLength(100);
            builder.Property(a => a.Description).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(500);
            builder.Property(a => a.Color).IsRequired(false);

            builder.HasOne<Seller>(a => a.Seller).WithMany(a => a.Products).HasForeignKey(a => a.SellerID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Brand>(a => a.Brand).WithMany(a => a.Products).HasForeignKey(a => a.BrandID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<ShoppingCart>(a => a.ShoppingCart).WithMany(a => a.Products).HasForeignKey(a => a.ShoppingCartID).OnDelete(DeleteBehavior.NoAction);


            builder.ToTable("Products");
        }
    }
}
