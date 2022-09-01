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
    public class FavoriteAndCustomerMap : IEntityTypeConfiguration<FavoriteAndCustomer>
    {
        public void Configure(EntityTypeBuilder<FavoriteAndCustomer> builder)
        {
            builder.HasKey(fac => new { fac.CustomerID, fac.ProductID });

            builder.HasOne<Customer>(gac => gac.Customer).WithMany(c => c.Favorites).HasForeignKey(gac => gac.CustomerID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<Product>(gac => gac.Product).WithMany(p => p.Favorites).HasForeignKey(gac => gac.ProductID).OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("FavoriteAndCustomers");
        }
    }
}
