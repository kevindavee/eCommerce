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
using eCommerce.Core.CommerceClasses.The_Products.Categories;

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
            if(product == null)
            {
                product = new Product();
            }
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
                var check = false;
                if(item.OptionName == "Size")
                {
                    if(product.SizeOption == true)
                    {
                        check = true;
                    }
                }
                else if(item.OptionName == "Warna")
                {
                    if (product.WarnaOption == true)
                    {
                        check = true;
                    }
                }
                viewModel.listOptions.Add(new OptionListViewModel { Options = item, Selected = check });
            }
            
            //Form untuk input product baru atau edit product
            return PartialView(viewModel);
        }

        public ActionResult SubmitProduct(DetailsProductViewModel model)
        {
            //Page untuk melihat list of product
            var product = productRepo.GetById(model.Product.Id);
            if(product == null)
            {
                product = new Product();
            }
            product.Nama = model.Product.Nama;
            product.Deskripsi = model.Product.Deskripsi;
            product.CategoryId = model.Product.CategoryId;
            product.DefaultPrice = model.Product.DefaultPrice;
            product.BrandId = model.Product.BrandId;
            product.CreatedBy = UserName;
            product.UpdatedBy = UserName;

            productRepo.Save(product);

            return RedirectToAction("ProductDetails", new { id = product.Id });
        }

        public string ChangeOptions(long productId, long id, bool check)
        {
            try
            {
                var option = optionRepo.GetById(id);
                var product = productRepo.GetById(productId);
                if (product == null)
                {
                    return "Save Product First!!";
                }
                if(option.OptionName == "Warna")
                {
                    if(check == true)
                    {
                        product.WarnaOption = true;
                    }
                    else
                    {
                        product.WarnaOption = false;
                    }
                }
                else if(option.OptionName == "Size")
                {
                    if(check == true)
                    {
                        product.SizeOption = true;
                    }
                    else
                    {
                        product.SizeOption = false;
                    }
                }
                productRepo.Save(product);
                var productInstanceList = productInstanceRepo.GetByProductId(product.Id).ToList();
                var deleteProductInstanceList = new List<ProductInstance>();
                foreach (var item in productInstanceList)
                {
                    var productInstanceOptionsList = productInstanceOptionsRepo.GetByProductInstanceIdAndOptionId(item.Id, option.Id).ToList();
                    if(productInstanceOptionsList.Count() > 0)
                    {
                        productInstanceOptionsRepo.DeleteListProductInstanceOptions(productInstanceOptionsList);
                    }
                    productInstanceOptionsList = productInstanceOptionsRepo.GetByProductInstanceId(item.Id).ToList();
                    if (productInstanceOptionsList.Count() == 0)
                    {
                        deleteProductInstanceList.Add(item);
                    }
                }
                if(deleteProductInstanceList.Count() > 0)
                {
                    productInstanceRepo.DeleteList(deleteProductInstanceList);
                }
                //lanjut disini
                return "1";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            //Page untuk melihat list of product
            
        }

        public JsonResult ListCheckedOption(long id = 0)
        {

            var product = productRepo.GetById(id);
            if(product == null)
            {
                product = new Product();
            }
            var listCheck = new List<bool>();

            listCheck.Add(product.WarnaOption);
            listCheck.Add(product.SizeOption);

            var result = new { checkedList = listCheck };

            return Json(result);
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

        #region Category
        public ActionResult ManageCategory()
        {
            return View();
        }

        public IActionResult CategoryList()
        {
            return ViewComponent("ManageCategoryList");
        }

        public ActionResult AddEditCategory(long CategoryId = 0)
        {
            var parent = new List<Category>();
            parent = categoryRepo.GetAll().Where(s => s.ParentId == null).ToList();
            parent.Insert(0, new Category { Id = 0, Nama = "No Parent Category" });

            ViewBag.Parent = parent;
            Category category;
            if (CategoryId == 0)
            {
                category = new Category();
            }
            else
            {
                category = categoryRepo.GetById(CategoryId);
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditCategory(Category category)
        {
            if (category.Id != 0)
            {
                category.UpdatedBy = "Admin";
                category.UpdatedDate = DateTime.Today;
            }

            if (category.ParentId == 0)
            {
                category.ParentId = null;
            }

            categoryRepo.Save(category);

            return RedirectToAction("ManageCategory");
        }
        #endregion

        public ActionResult ManageBrand()
        {
            return View();
        }

        public IActionResult BrandList()
        {
            return ViewComponent("ManageBrandList");
        }

        public ActionResult AddEditBrand(long BrandId = 0)
        {
            Brand brand;
            if (BrandId == 0)
            {
                brand = new Brand();
            }
            else
            {
                brand = brandRepo.GetById(BrandId);
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEditBrand(Brand brand)
        {
            if (brand.Id != 0)
            {
                brand.UpdatedBy = "Admin";
                brand.UpdatedDate = DateTime.Today;
            }

            brandRepo.Save(brand);
            return RedirectToAction("ManageBrand");
        }

    }
}