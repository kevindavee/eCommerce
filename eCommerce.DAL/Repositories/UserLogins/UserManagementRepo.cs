using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eCommerce.DAL.Repositories.UserLogins
{
    public class UserManagementRepo
    {
        private CommerceContext context;

        public UserManagementRepo(CommerceContext _context)
        {
            context = _context;
        }

        public string GetRoleName(string userName)
        {
            var currentUser = context.Users.Where(i => i.UserName == userName).FirstOrDefault();
            var roleId = currentUser.Roles.Where(i => i.UserId == currentUser.Id).FirstOrDefault().RoleId;
            return context.Roles.Where(i => i.Id == roleId).FirstOrDefault().Name;
        }

        public long GetCustomerId(string userName)
        {
            var currentUser = context.Users.Where(i => i.UserName == userName).FirstOrDefault();
            return currentUser.ObjectId;
        }
    }
}
