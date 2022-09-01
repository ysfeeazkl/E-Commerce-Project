using E_Commerce.Data.Concrete.Mappings;
using E_Commerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Data.Concrete.Context
{
    public class CommerceContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<BrandPicture> BrandPictures { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryAndProduct> CategoryAndProducts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerPicture> CustomerPictures { get; set; }
        public DbSet<FavoriteAndCustomer> FavoriteAndCustomers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPicture> ProductPictures{ get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportPicture> ReportPictures { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SellerPicture> SellerPictures { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<CustomerAndOperationClaim> CustomerAndOperationClaims { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandMap());
            modelBuilder.ApplyConfiguration(new BrandPictureMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CategoryAndProductMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new CustomerPictureMap());
            modelBuilder.ApplyConfiguration(new FavoriteAndCustomerMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ProductPictureMap());
            modelBuilder.ApplyConfiguration(new ReportMap());
            modelBuilder.ApplyConfiguration(new ReportPictureMap());
            modelBuilder.ApplyConfiguration(new SellerMap());
            modelBuilder.ApplyConfiguration(new SellerPictureMap());
            modelBuilder.ApplyConfiguration(new ShoppingCartMap());
            modelBuilder.ApplyConfiguration(new OperationClaimMap());
            modelBuilder.ApplyConfiguration(new CustomerAndOperationClaimMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=CommerceDb;Trusted_Connection=True;");
        }
    }
}
