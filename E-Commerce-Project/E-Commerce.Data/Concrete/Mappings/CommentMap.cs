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
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(a => a.ID).ValueGeneratedOnAdd();
            builder.Property(c => c.Content).IsRequired();
            builder.Property(c => c.Content).HasMaxLength(400);

            builder.HasOne<Customer>(c => c.Customer).WithMany(c => c.Comments).HasForeignKey(c => c.CustomerID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Product>(c => c.Product).WithMany(c => c.Comments).HasForeignKey(c => c.ProductID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Seller>(c => c.Seller).WithMany(c => c.Comments).HasForeignKey(c => c.SellerID).OnDelete(DeleteBehavior.NoAction); 
            builder.HasOne<Comment>(c => c.BaseComment).WithMany(c => c.Comments).HasForeignKey(c => c.BaseCommentID).OnDelete(DeleteBehavior.NoAction); 

            builder.ToTable("Comments");
        }
    }
}
