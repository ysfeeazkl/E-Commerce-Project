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
    public class CustomerAndOperationClaimMap : IEntityTypeConfiguration<CustomerAndOperationClaim>
    {
        public void Configure(EntityTypeBuilder<CustomerAndOperationClaim> builder)
        {
            builder.HasKey(a => new { a.CustomerID, a.OperationClaimID });
            builder.HasOne<Customer>(uo => uo.Customer).WithMany(u => u.CustomerAndOperationClaims).HasForeignKey(uo => uo.CustomerID);
            builder.HasOne<OperationClaim>(a => a.OperationClaim).WithMany(a => a.CustomerAndOperationClaims).HasForeignKey(a => a.OperationClaimID);

            builder.ToTable("CustomerAndOperationClaims");
        }
    }
}
