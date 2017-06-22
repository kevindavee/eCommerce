using eCommerce.Core.CommerceClasses.The_Products.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.Models.AdminProduct
{
    public class CategoryListViewModel
    {
        public List<Category> Category { get; set; }
        public List<long?> SubCategoryParentIdList { get; set; }
    }
}
