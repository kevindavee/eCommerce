using eCommerce.Core.CommerceClasses.UserLogins;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.DAL.Configuration
{
    public class UserRoleSeed
    {
        private readonly RoleManager<RolesMaster> _roleManager;
        private readonly UserManager<UserLogin> _userLogin;

        public UserRoleSeed(RoleManager<RolesMaster> roleManager, UserManager<UserLogin> userLogin)
        {
            _roleManager = roleManager;
            _userLogin = userLogin;
        }

        public async void Seed()
        {
            if ((await _roleManager.FindByNameAsync("SuperAdmin")) == null)
            {
                await _roleManager.CreateAsync(new RolesMaster { Name = "SuperAdmin" });

                var user = new UserLogin
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    ObjectId = 0
                };

                var admin = await _userLogin.CreateAsync(user, "Admin123");
                if (admin.Succeeded)
                {
                    var result = await _userLogin.AddToRoleAsync(user, "SuperAdmin");
                }
            }

            if ((await _roleManager.FindByNameAsync("Customer")) == null)
            {
                await _roleManager.CreateAsync(new RolesMaster { Name = "Customer" });
            }

            if ((await _roleManager.FindByNameAsync("ProductAdmin")) == null)
            {
                await _roleManager.CreateAsync(new RolesMaster { Name = "ProductAdmin" });
            }

            if ((await _roleManager.FindByNameAsync("FinanceAdmin")) == null)
            {
                await _roleManager.CreateAsync(new RolesMaster { Name = "FinanceAdmin" });
            }
        }
    }
}
