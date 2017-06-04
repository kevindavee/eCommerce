using eCommerce.Core.CommerceClasses.Alamats;
using eCommerce.Core.CommerceClasses.Banks;
using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.BrandsAndCategories;
using eCommerce.Core.CommerceClasses.Customers;
using eCommerce.Core.CommerceClasses.The_Products.Categories;
using eCommerce.Core.CommerceClasses.The_Products.Product_Images;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using eCommerce.Core.CommerceClasses.The_Products.Reviews;
using eCommerce.Core.CommerceClasses.Transactions.KonfirmasiPembayarans;
using eCommerce.Core.CommerceClasses.Transactions.ShippingDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionDetailss;
using eCommerce.Core.CommerceClasses.Transactions.TransactionHeaders;
using eCommerce.Core.CommerceClasses.UserLogins;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL
{
    public class CommerceContext : IdentityDbContext<UserLogin, RolesMaster, string>
    {
        public CommerceContext(DbContextOptions<CommerceContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ProductInstanceOptions>()
                .HasKey(c => new { c.ProductInstanceId, c.OptionValueId});
        }

        public DbSet<Brand> Brand { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BrandAndCategory> BrandAndCategory { get; set; }
        public DbSet<ProductInstance> ProductInstance { get; set; }
        public DbSet<ProductInstanceOptions> ProductInstanceOptions { get; set; }
        public DbSet<OptionValue> OptionValue { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Alamat> Alamat { get; set; }
        public DbSet<TransactionHeader> TransactionHeader { get; set; }
        public DbSet<TransactionDetails> TransactionDetails { get; set; }
        public DbSet<ShippingDetails> ShippingDetails { get; set; }
        public DbSet<KonfirmasiPembayaran> KonfirmasiPembayaran { get; set; }
        public DbSet<Bank> Bank { get; set; }
    }
}
