using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using eCommerce.DAL;

namespace eCommerce.DAL.Migrations
{
    [DbContext(typeof(CommerceContext))]
    partial class CommerceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.Alamats.Alamat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CustomerId");

                    b.Property<int>("KodePos");

                    b.Property<string>("Kota");

                    b.Property<string>("Provinsi");

                    b.Property<string>("TheAlamat");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Alamat");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.Brands.Brand", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Nama");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.BrandsAndCategories.BrandAndCategory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BrandId");

                    b.Property<long>("CategoryId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BrandAndCategory");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.Customers.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("Foto");

                    b.Property<bool>("JenisKelamin");

                    b.Property<string>("Nama");

                    b.Property<string>("NoTelepon");

                    b.Property<string>("Pekerjaan");

                    b.Property<bool>("StatusNikah");

                    b.Property<DateTime>("TanggalLahir");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Categories.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Nama");

                    b.Property<long>("ParentId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Product_Images.ProductImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Path");

                    b.Property<long>("ProductId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Products.Options", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("OptionName");

                    b.Property<long>("ProductId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Products.OptionValue", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("OptionId");

                    b.Property<long?>("OptionsId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("OptionsId");

                    b.ToTable("OptionValue");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Products.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("BrandId");

                    b.Property<long>("CategoryId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Deskripsi");

                    b.Property<string>("Nama");

                    b.Property<float>("Rating");

                    b.Property<long>("RatingCount");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Products.ProductInstance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("Price");

                    b.Property<long>("ProductId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductInstance");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Products.ProductInstanceOptions", b =>
                {
                    b.Property<long>("ProductInstanceId");

                    b.Property<long>("OptionValueId");

                    b.HasKey("ProductInstanceId", "OptionValueId");

                    b.HasIndex("OptionValueId");

                    b.ToTable("ProductInstanceOptions");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Reviews.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<long>("CustomerId");

                    b.Property<long>("ProductId");

                    b.Property<DateTime>("TanggalReview");

                    b.Property<string>("TheReview");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.UserLogins.RolesMaster", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.UserLogins.UserLogin", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<long>("ObjectId");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.Alamats.Alamat", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.Customers.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.BrandsAndCategories.BrandAndCategory", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.Brands.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eCommerce.Core.CommerceClasses.The_Products.Categories.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Product_Images.ProductImage", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.The_Products.Products.Product", "Product")
                        .WithMany("ProductImage")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Products.Options", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.The_Products.Products.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Products.OptionValue", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.The_Products.Products.Options", "Options")
                        .WithMany("OptionValue")
                        .HasForeignKey("OptionsId");
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Products.Product", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.Brands.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eCommerce.Core.CommerceClasses.The_Products.Categories.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Products.ProductInstance", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.The_Products.Products.Product", "Product")
                        .WithMany("ProductInstance")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Products.ProductInstanceOptions", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.The_Products.Products.OptionValue", "OptionValue")
                        .WithMany("ProductInstanceOptions")
                        .HasForeignKey("OptionValueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eCommerce.Core.CommerceClasses.The_Products.Products.ProductInstance", "ProductInstance")
                        .WithMany("ProductInstanceOptions")
                        .HasForeignKey("ProductInstanceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("eCommerce.Core.CommerceClasses.The_Products.Reviews.Review", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.Customers.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eCommerce.Core.CommerceClasses.The_Products.Products.Product", "Product")
                        .WithMany("Review")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.UserLogins.RolesMaster")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.UserLogins.UserLogin")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.UserLogins.UserLogin")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("eCommerce.Core.CommerceClasses.UserLogins.RolesMaster")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("eCommerce.Core.CommerceClasses.UserLogins.UserLogin")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
