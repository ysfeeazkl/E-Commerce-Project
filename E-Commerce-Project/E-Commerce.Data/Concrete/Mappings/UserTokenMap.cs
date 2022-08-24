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
    public class UserTokenMap : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Token).IsRequired();
            builder.Property(a => a.TokenExpiration).IsRequired();
            builder.Property(a => a.CustomerID).IsRequired();

            builder.HasOne<Customer>(a => a.Customer).WithMany(a => a.UserTokens).HasForeignKey(a => a.CustomerID);
            builder.ToTable("UserTokens");
        }
    }
}
