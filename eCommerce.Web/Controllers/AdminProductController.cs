using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL.Repositories.Brands;
using eCommerce.DAL.Repositories.The_Products.Products;
using Microsoft.AspNetCore.Authorization;
using eCommerce.Web.Models.AdminProduct;
using eCommerce.DAL.Repositories.The_Products.Categories;
using eCommerce.DAL.Repositories.BrandsAndCategories;
using eCommerce.Core.CommerceClasses.Brands;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using Microsoft.AspNetCore.Http;

namespace eCommerce.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminProductController : Controller
    {
        private BrandRepo brandRepo;
        private BrandAndCategoryRepo brAndCatRepo;
        private CategoryRepo categoryRepo;
        private ProductRepo productRepo;
        private OptionsRepo optionRepo;
        private ProductInstanceRepo productInstanceRepo;
        private ProductInstanceOptionsRepo productInstanceOptionsRepo;
        private IHttpContextAccessor context;

        private string UserName = "";

        public AdminProductController(BrandRepo _brandRepo, CategoryRepo _categoryRepo, ProductRepo _productRepo, ProductInstanceRepo _productInstanceRepo, ProductInstanceOptionsRepo _productInstanceOptionsRepo
                                       , OptionsRepo _optionRepo, BrandAndCategoryRepo _brAndCatRepo, IHttpContextAccessor _context)
        {
            brandRepo = _brandRepo;
            this.productRepo = _productRepo;
            this.productInstanceRepo = _productInstanceRepo;
            this.productInstanceOptionsRepo = _productInstanceOptionsRepo;
            categoryRepo = _categoryRepo;
            optionRepo = _optionRepo;
            brAndCatRepo = _brAndCatRepo;
            context = _context;
            UserName = context.HttpContext.User.Identity.Name;
        }
        public ActionResult Index()
        {
            var viewModel = new IndexPageAdminProductViewModel();
            var productList = productRepo.GetAllProduct().ToList();

            viewModel.listProduct = productList;
            viewModel.DetailsProduct.listCategory = categoryRepo.GetAll().Where(i => i.ParentId == null).ToList();
            var listOption = optionRepo.GetAll();
            foreach (var item in listOption)
            {
                viewModel.DetailsProduct.listOptions.Add(new OptionListViewModel { Options = item });
            }
            return View(viewModel);
        }
        public PartialViewResult ManageProduct()
        {
            //Page untuk melihat list of product
            var productList = productRepo.GetAllProduct().ToList();

            return PartialView(productList);
        }

        public ActionResult DeleteProduct(long id = 0)
        {
            //Page untuk melihat list of product
            productRepo.Delete(id);

            return RedirectToAction("ManageProduct");
        }

        public PartialViewResult ProductDetails(long id = 0)
        {
            var viewModel = new DetailsProductViewModel();
            var product = productRepo.GetByIdIncludeCat(id);
            viewModel.Product = product;
            if(product.Category != null)
            {
                viewModel.CategoryId = (long)product.Category.ParentId;
            }
            viewModel.listCategory = categoryRepo.GetAll().Where(i => i.ParentId == null).ToList();
            viewModel.listSubCategory = categoryRepo.GetAll().Where(i => i.ParentId != null).ToList();
            var brAndCatList = brAndCatRepo.GetByCategoryId(product.CategoryId).ToList();
            foreach (var item in brAndCatList)
            {
                viewModel.listBrand.Add(brandRepo.GetById(item.BrandId));
            }
            
            var listOption = optionRepo.GetAll();
            foreach (var item in listOption)
            {
                viewModel.listOptions.Add(new OptionListViewModel { Options = item });
            }
            
            //Form untuk input product baru atau edit product
            return PartialView(viewModel);
        }

        public ActionResult SubmitProduct(DetailsProductViewModel model)
        {
            //Page untuk melihat list of product
            var product = new Product();
            product.Nama = model.Product.Nama;
            product.Deskripsi = model.Product.Deskripsi;
            product.CategoryId = model.Product.CategoryId;
            product.BrandId = model.Product.BrandId;
            product.CreatedBy = UserName;
            product.UpdatedBy = UserName;

            productRepo.Save(product);

            return RedirectToAction("ProductDetails", new { id = product.Id });
        }

        public JsonResult GetSubCategory(long CategoryId = 0)
        {

            var subCategoryList = categoryRepo.GetAll().Where(i => i.ParentId == CategoryId).ToList();

            var result = new { subCategoryList = subCategoryList };

            return Json(result);
        }

        public JsonResult GetBrandListByCategory(long subCategoryId = 0)
        {

            var brandAndCatList = brAndCatRepo.GetByCategoryId(subCategoryId).ToList();
            var brandList = new List<Brand>();
            foreach (var item in brandAndCatList)
            {
                var brand = brandRepo.GetById(item.BrandId);
                brandList.Add(brand);
            }
            var result = new { brandList = brandList };

            return Json(result);
        }

        public PartialViewResult ProductList()
        {
            //Partial page untuk refresh hasil filter product
            return PartialView();
        }

        public ActionResult UpdateProduct()
        {
            //Form untuk input product baru atau edit product
            return View();
        }

        
    }
}