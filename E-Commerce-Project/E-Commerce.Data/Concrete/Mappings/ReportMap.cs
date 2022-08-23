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
    public class ReportMap : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(a => a.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.Content).IsRequired();
            builder.Property(c => c.Content).HasMaxLength(400);

            builder.HasOne<Customer>(c => c.Customer).WithMany(c => c.Reports).HasForeignKey(c => c.CustomerID);
            builder.HasOne<Comment>(c => c.Comment).WithMany(c => c.Reports).HasForeignKey(c => c.CommentID);
            builder.HasOne<Product>(c => c.Product).WithMany(c => c.Reports).HasForeignKey(c => c.ProductID);
            builder.HasOne<Seller>(c => c.Seller).WithMany(c => c.Reports).HasForeignKey(c => c.SellerID);
            builder.HasOne<Brand>(c => c.Brand).WithMany(c => c.Reports).HasForeignKey(c => c.BrandID);
          

            builder.ToTable("Reports");
        }
    }
}
