using eCommerce.Core.CommerceClasses.Customers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eCommerce.Core.CommerceClasses.UserLogins
{
    public class UserLogin : IdentityUser
    {
        public long ObjectId { get; set; }
    }
}
