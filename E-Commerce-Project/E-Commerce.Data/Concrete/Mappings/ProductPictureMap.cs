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
    public class ProductPictureMap : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).ValueGeneratedOnAdd();

            builder.Property(a => a.FileName).IsRequired();
            builder.Property(a => a.FilePath).IsRequired();

            builder.HasOne<Product>(pp => pp.Product).WithMany(p => p.ProductPictures).HasForeignKey(p => p.ProductID);

            builder.ToTable("ProductPictures");
        }
    }
}
