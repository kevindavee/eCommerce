using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.AccountViewModels
{
    public class RegisterAdminViewModel: RegisterViewModel
    {
        public int AdminRole { get; set; } = 0;
    }
}
