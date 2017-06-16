using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using eCommerce.Web.Models;
using eCommerce.Web.Services;
using eCommerce.DAL;
using eCommerce.Core.CommerceClasses.UserLogins;
using eCommerce.DAL.Configuration;
using Microsoft.AspNetCore.Identity;
using eCommerce.Commons;
using eCommerce.DAL.Repositories.Customers;
using eCommerce.DAL.Repositories.Brands;
using eCommerce.DAL.Repositories.Alamats;
using eCommerce.DAL.Repositories.The_Products.Products;
using eCommerce.DAL.Repositories.The_Products.Reviews;
using eCommerce.DAL.Repositories.Banks;
using eCommerce.DAL.Repositories.BrandsAndCategories;
using eCommerce.DAL.Repositories.Shippers;
using eCommerce.DAL.Repositories.Transactions.TransactionHeaders;
using eCommerce.DAL.Repositories.Transactions.TransactionDetailss;
using eCommerce.DAL.Repositories.Transactions.ShippingDetailss;
using eCommerce.DAL.Repositories.Transactions.KonfirmasiPembayarans;
using eCommerce.Logic.Services;
using eCommerce.DAL.Repositories.UserLogins;
using eCommerce.DAL.Repositories.The_Products.Categories;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<CommerceContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UserLogin, RolesMaster>()
                .AddEntityFrameworkStores<CommerceContext>()
                .AddDefaultTokenProviders();

            services.AddMvc(options =>
            {
                options.SslPort = 44343;
                options.Filters.Add(new RequireHttpsAttribute());
            });

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            //Register Application Repository
            services.AddScoped<CustomerRepo, CustomerRepo>();
            services.AddScoped<BrandRepo, BrandRepo>();
            services.AddScoped<CategoryRepo, CategoryRepo>();
            services.AddScoped<AlamatRepo, AlamatRepo>();
            services.AddScoped<ProductRepo, ProductRepo>();
            services.AddScoped<ReviewRepo, ReviewRepo>();
            services.AddScoped<OptionValueRepo, OptionValueRepo>();
            services.AddScoped<ProductInstanceRepo, ProductInstanceRepo>();
            services.AddScoped<ProductInstanceOptionsRepo, ProductInstanceOptionsRepo>();
            services.AddScoped<OptionsRepo, OptionsRepo>();
            services.AddScoped<BankRepo, BankRepo>();
            services.AddScoped<BrandAndCategoryRepo, BrandAndCategoryRepo>();
            services.AddScoped<ShipperRepo, ShipperRepo>();
            services.AddScoped<TransactionHeaderRepo, TransactionHeaderRepo>();
            services.AddScoped<TransactionDetailsRepo, TransactionDetailsRepo>();
            services.AddScoped<ShippingDetailsRepo, ShippingDetailsRepo>();
            services.AddScoped<KonfirmasiPembayaranRepo, KonfirmasiPembayaranRepo>();
            services.AddScoped<UserManagementRepo, UserManagementRepo>();


            services.AddScoped<TransactionService, TransactionService>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // Cookie settings
                options.Cookies.ApplicationCookie.CookieName = "YouAppCookieName";
                options.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromDays(150);
                options.Cookies.ApplicationCookie.LoginPath = "/Account/LogIn";
                options.Cookies.ApplicationCookie.LogoutPath = "/Account/LogOff";
                options.Cookies.ApplicationCookie.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                options.Cookies.ApplicationCookie.AuthenticationScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();
            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = Configuration["Authentication:Facebook:AppId"],
                AppSecret = Configuration["Authentication:Facebook:AppSecret"]
            });

            //new UserRoleSeed(app.ApplicationServices.GetService<RoleManager<RolesMaster>>(), app.ApplicationServices.GetService<UserManager<UserLogin>>()).Seed();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
