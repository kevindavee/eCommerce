using eCommerce.DAL.Repositories.The_Products.Categories;
using eCommerce.Web.Models.AdminProduct;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.ViewComponents.AdminProduct
{
    public class ManageCategoryListViewComponent: ViewComponent
    {
        private CategoryRepo categoryRepo;

        public ManageCategoryListViewComponent(CategoryRepo _categoryRepo)
        {
            categoryRepo = _categoryRepo;
        }

        public IViewComponentResult Invoke()
        {
            CategoryListViewModel viewmodel = new CategoryListViewModel();
            viewmodel.Category = categoryRepo.GetAll();

            viewmodel.SubCategoryParentIdList = (from x in viewmodel.Category
                                                where x.ParentId != null
                                                orderby x.ParentId
                                                select x.ParentId).Distinct().ToList();

            return View("ManageCategoryList", viewmodel);
        }
    }
}
