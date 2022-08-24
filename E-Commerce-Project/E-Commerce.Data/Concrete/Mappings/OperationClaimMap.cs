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
    public class OperationClaimMap : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).ValueGeneratedOnAdd();
            builder.ToTable("OperationClaims");

            builder.HasData(new OperationClaim()
            {
                ID = 1,
                Name = "Admin",
                CreatedDate = DateTime.Now,
            },
            new OperationClaim()
            {
                ID = 2,
                Name = "Moderator",
                CreatedDate = DateTime.Now,
            },
            new OperationClaim()
            {
                ID = 3,
                Name = "Seller",
                CreatedDate = DateTime.Now,
            },
            new OperationClaim()
            {
                ID = 4,
                Name = "Customer",
                CreatedDate = DateTime.Now,
            });

            builder.ToTable("OperationClaims");
        }
    }
}
