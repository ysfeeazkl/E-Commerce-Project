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
    public class CustomerPictureMap : IEntityTypeConfiguration<CustomerPicture>
    {
        public void Configure(EntityTypeBuilder<CustomerPicture> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).ValueGeneratedOnAdd();

            builder.Property(a => a.FileName).IsRequired();
            builder.Property(a => a.FilePath).IsRequired();

            builder.HasOne<Customer>(a => a.Customer).WithOne(a => a.CustomerPicture).HasForeignKey<Customer>(c => c.CustomerPictureID);

            builder.ToTable("CustomerPictures");
        }
    }
}
