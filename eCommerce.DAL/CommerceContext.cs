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

        public CommerceContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
