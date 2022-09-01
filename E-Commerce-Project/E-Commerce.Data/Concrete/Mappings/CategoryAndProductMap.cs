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
    public class CategoryAndProductMap : IEntityTypeConfiguration<CategoryAndProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryAndProduct> builder)
        {
            builder.HasKey(cap => new { cap.CategoryID, cap.ProductID});

            builder.HasOne<Category>(gac => gac.Category).WithMany(c => c.CategoryAndProducts).HasForeignKey(gac => gac.CategoryID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Product>(gac => gac.Product).WithMany(p => p.CategoryAndProducts).HasForeignKey(gac => gac.ProductID).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("CategoryAndProducts");
        }
    }
}
