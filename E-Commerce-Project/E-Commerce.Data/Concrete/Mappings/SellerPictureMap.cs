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
    public class SellerPictureMap : IEntityTypeConfiguration<SellerPicture>
    {
        public void Configure(EntityTypeBuilder<SellerPicture> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).ValueGeneratedOnAdd();

            builder.Property(a => a.FileName).IsRequired();
            builder.Property(a => a.FilePath).IsRequired();

            builder.HasOne<Seller>(a => a.Seller).WithOne(a => a.SellerPicture).HasForeignKey<Seller>(c => c.SellerPictureID);

            builder.ToTable("SellerPictures");
        }
    }
}
