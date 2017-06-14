using eCommerce.DAL.Repositories.The_Products.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerce.Web.ViewComponents
{
    public class CategoryListViewComponent : ViewComponent
    {
        private CategoryRepo categoryRepo;

        public CategoryListViewComponent(CategoryRepo _categoryRepo)
        {
            categoryRepo = _categoryRepo;
        }

        public IViewComponentResult Invoke()
        {
            var categories = categoryRepo.GetAll();
            return View("CategoryList", categories);
        }
    }
}
