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
using eCommerce.Core.CommerceClasses.BrandsAndCategories;
using eCommerce.DAL.Repositories.Stocks;
using eCommerce.Core.CommerceClasses.Stocks;

namespace eCommerce.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin, ProductAdmin")]
    public class AdminProductController : Controller
    {
        private BrandRepo brandRepo;
        private BrandAndCategoryRepo brAndCatRepo;
        private CategoryRepo categoryRepo;
        private StockRepo stockRepo;
        private ProductRepo productRepo;
        private OptionsRepo optionRepo;
        private OptionValueRepo optionValueRepo;
        private ProductInstanceRepo productInstanceRepo;
        private ProductInstanceOptionsRepo productInstanceOptionsRepo;
        private IHttpContextAccessor context;

        private string UserName = "";

        public AdminProductController(BrandRepo _brandRepo, CategoryRepo _categoryRepo, ProductRepo _productRepo, ProductInstanceRepo _productInstanceRepo, ProductInstanceOptionsRepo _productInstanceOptionsRepo
                                       , OptionsRepo _optionRepo, OptionValueRepo _optionValueRepo, BrandAndCategoryRepo _brAndCatRepo, StockRepo _stockRepo, IHttpContextAccessor _context)
        {
            brandRepo = _brandRepo;
            this.productRepo = _productRepo;
            this.productInstanceRepo = _productInstanceRepo;
            this.productInstanceOptionsRepo = _productInstanceOptionsRepo;
            categoryRepo = _categoryRepo;
            optionRepo = _optionRepo;
            optionValueRepo = _optionValueRepo;
            brAndCatRepo = _brAndCatRepo;
            stockRepo = _stockRepo;
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
            if(product.ProductInstance == null)
            {
                product.ProductInstance = new List<ProductInstance>();
            }
            foreach (var item in product.ProductInstance)
            {
                var productInstance = productInstanceRepo.GetByIdIncludeOptions(item.Id);
                var nama = "";
                foreach (var item1 in productInstance.ProductInstanceOptions)
                {
                    var optionValue = optionValueRepo.GetById(item1.OptionValueId);
                    nama = nama + optionValue.Value + ", ";
                }
                viewModel.listProductOption.Add(new ListProductOption { Id = item.Id, Nama = nama });
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

        public ActionResult DeleteProductOption(long id = 0)
        {
            //Page untuk melihat list of product
            var productInstance = productInstanceRepo.GetById(id);
            long productid = 0;
            if(productInstance != null)
            {
                productid = productInstance.ProductId;
                productInstanceRepo.Delete(id);
            }
            

            return RedirectToAction("ProductDetails", new { id = productid });
        }

        public ActionResult AddOptionWarna(string warna = "", long productId = 0)
        {
            //Page untuk melihat list of product
            try
            {
                var product = productRepo.GetByIdIncludeCat(productId);
                var option = optionRepo.GetAll().Where(i => i.OptionName == "Warna").FirstOrDefault();
                if(product.ProductInstance.Count() > 0)
                {
                    bool check = false;
                    bool check1 = false;
                    bool check2 = false;
                    //int count = 0;
                    var listOptionSize = new List<OptionValue>();
                    var listProductInstance = new List<ProductInstance>();
                    foreach (var item in product.ProductInstance)
                    {
                        var productInstance = productInstanceRepo.GetByIdIncludeOptions(item.Id);
                        if(productInstance.ProductInstanceOptions.Count() > 1)
                        {
                            check2 = true;
                        }
                        //var productInstanceOption = productInstanceOptionsRepo.GetByProductInstanceId(item.Id).ToList();
                        foreach (var item1 in productInstance.ProductInstanceOptions)
                        {
                            var optionValue = optionValueRepo.GetById(item1.OptionValueId);
                            if (optionValue.Value.ToLower().Trim() == warna.ToLower().Trim())
                            {
                                check = true;
                            }
                            if(optionValue.OptionsId != option.Id)
                            {
                                check1 = true;
                                var sizeVal = listOptionSize.Where(i => i.Value == optionValue.Value).FirstOrDefault();
                                if(sizeVal == null)
                                {
                                    listOptionSize.Add(optionValue);
                                    listProductInstance.Add(productInstance);
                                }
                            }
                        }
                    }
                    if(check == false)
                    {
                        if(check1 == true)
                        {
                            if(check2 == true)
                            {
                                foreach (var item in listOptionSize)
                                {
                                    var productInstance = new ProductInstance();
                                    productInstance.ProductId = product.Id;
                                    productInstance.Price = product.DefaultPrice;
                                    productInstance.CreatedBy = UserName;
                                    productInstance.UpdatedBy = UserName;

                                    productInstanceRepo.Save(productInstance);

                                    var stock = stockRepo.GetByProductInstanceId(productInstance.Id);
                                    if(stock == null)
                                    {
                                        stock = new Stock();
                                        stock.CreatedBy = UserName;
                                    }
                                    stock.ProductInstanceId = productInstance.Id;
                                    stock.UpdatedBy = UserName;
                                    stock.UpdatedDate = DateTime.Now;

                                    stockRepo.Save(stock);

                                    var optionValue = optionValueRepo.GetAll().Where(i => i.OptionsId == option.Id && i.Value.ToLower().Trim() == warna.ToLower().Trim()).FirstOrDefault();
                                    if (optionValue == null)
                                    {
                                        optionValue = new OptionValue();
                                        optionValue.Value = warna;
                                        optionValue.OptionsId = option.Id;
                                        optionValue.CreatedBy = UserName;
                                        optionValue.UpdatedBy = UserName;
                                        optionValueRepo.Save(optionValue);
                                    }

                                    var productInstanceOption = new ProductInstanceOptions();
                                    productInstanceOption.OptionValueId = optionValue.Id;
                                    productInstanceOption.ProductInstanceId = productInstance.Id;
                                    productInstanceOptionsRepo.Save(productInstanceOption);

                                    productInstanceOption = new ProductInstanceOptions();
                                    productInstanceOption.OptionValueId = item.Id;
                                    productInstanceOption.ProductInstanceId = productInstance.Id;
                                    productInstanceOptionsRepo.Save(productInstanceOption);
                                }
                            }
                            else
                            {
                                var y = 0;
                                foreach (var item in listOptionSize)
                                {
                                    var optionValue = optionValueRepo.GetAll().Where(i => i.OptionsId == option.Id && i.Value.ToLower().Trim() == warna.ToLower().Trim()).FirstOrDefault();
                                    if (optionValue == null)
                                    {
                                        optionValue = new OptionValue();
                                        optionValue.Value = warna;
                                        optionValue.OptionsId = option.Id;
                                        optionValue.CreatedBy = UserName;
                                        optionValue.UpdatedBy = UserName;
                                        optionValueRepo.Save(optionValue);
                                    }

                                    var productInstanceOption = new ProductInstanceOptions();
                                    productInstanceOption.OptionValueId = optionValue.Id;
                                    productInstanceOption.ProductInstanceId = listProductInstance[y].Id;
                                    productInstanceOptionsRepo.Save(productInstanceOption);
                                    y++;
                                }
                            }
                        }
                        else
                        {
                            var productInstance = new ProductInstance();
                            productInstance.ProductId = product.Id;
                            productInstance.Price = product.DefaultPrice;
                            productInstance.CreatedBy = UserName;
                            productInstance.UpdatedBy = UserName;

                            productInstanceRepo.Save(productInstance);

                            var stock = stockRepo.GetByProductInstanceId(productInstance.Id);
                            if (stock == null)
                            {
                                stock = new Stock();
                                stock.CreatedBy = UserName;
                            }
                            stock.ProductInstanceId = productInstance.Id;
                            stock.UpdatedBy = UserName;
                            stock.UpdatedDate = DateTime.Now;

                            stockRepo.Save(stock);

                            var optionValue = optionValueRepo.GetAll().Where(i => i.OptionsId == option.Id && i.Value.ToLower().Trim() == warna.ToLower().Trim()).FirstOrDefault();
                            if (optionValue == null)
                            {
                                optionValue = new OptionValue();
                                optionValue.Value = warna;
                                optionValue.OptionsId = option.Id;
                                optionValue.CreatedBy = UserName;
                                optionValue.UpdatedBy = UserName;
                                optionValueRepo.Save(optionValue);
                            }

                            var productInstanceOption = new ProductInstanceOptions();
                            productInstanceOption.OptionValueId = optionValue.Id;
                            productInstanceOption.ProductInstanceId = productInstance.Id;
                            productInstanceOptionsRepo.Save(productInstanceOption);
                        }
                    }
                }
                else
                {
                    var productInstance = new ProductInstance();
                    productInstance.ProductId = product.Id;
                    productInstance.Price = product.DefaultPrice;
                    productInstance.CreatedBy = UserName;
                    productInstance.UpdatedBy = UserName;

                    productInstanceRepo.Save(productInstance);

                    var stock = stockRepo.GetByProductInstanceId(productInstance.Id);
                    if (stock == null)
                    {
                        stock = new Stock();
                        stock.CreatedBy = UserName;
                    }
                    stock.ProductInstanceId = productInstance.Id;
                    stock.UpdatedBy = UserName;
                    stock.UpdatedDate = DateTime.Now;

                    stockRepo.Save(stock);

                    var optionValue = optionValueRepo.GetAll().Where(i => i.OptionsId == option.Id && i.Value.ToLower().Trim() == warna.ToLower().Trim()).FirstOrDefault();
                    if(optionValue == null)
                    {
                        optionValue = new OptionValue();
                        optionValue.Value = warna;
                        optionValue.OptionsId = option.Id;
                        optionValue.CreatedBy = UserName;
                        optionValue.UpdatedBy = UserName;
                        optionValueRepo.Save(optionValue);
                    }

                    var productInstanceOption = new ProductInstanceOptions();
                    productInstanceOption.OptionValueId = optionValue.Id;
                    productInstanceOption.ProductInstanceId = productInstance.Id;
                    productInstanceOptionsRepo.Save(productInstanceOption);

                }


                return RedirectToAction("ProductDetails", new { id = product.Id });
            }
            catch (Exception ex)
            {

                return new StatusCodeResult(500);
            }
        }

        public ActionResult AddOptionSize(string size = "", long productId = 0)
        {
            //Page untuk melihat list of product
            try
            {
                var product = productRepo.GetByIdIncludeCat(productId);
                var option = optionRepo.GetAll().Where(i => i.OptionName == "Size").FirstOrDefault();
                if (product.ProductInstance.Count() > 0)
                {
                    bool check = false;
                    bool check1 = false;
                    bool check2 = false;
                    //int count = 0;
                    var listOptionWarna = new List<OptionValue>();
                    var listProductInstance = new List<ProductInstance>();
                    foreach (var item in product.ProductInstance)
                    {
                        var productInstance = productInstanceRepo.GetByIdIncludeOptions(item.Id);
                        if (productInstance.ProductInstanceOptions.Count() > 1)
                        {
                            check2 = true;
                        }
                        //var productInstanceOption = productInstanceOptionsRepo.GetByProductInstanceId(item.Id).ToList();
                        foreach (var item1 in productInstance.ProductInstanceOptions)
                        {
                            var optionValue = optionValueRepo.GetById(item1.OptionValueId);
                            if (optionValue.Value.ToLower().Trim() == size.ToLower().Trim())
                            {
                                check = true;
                            }
                            if (optionValue.OptionsId != option.Id)
                            {
                                check1 = true;
                                var sizeVal = listOptionWarna.Where(i => i.Value == optionValue.Value).FirstOrDefault();
                                if (sizeVal == null)
                                {
                                    listOptionWarna.Add(optionValue);
                                    listProductInstance.Add(productInstance);
                                }
                            }
                        }
                    }
                    if (check == false)
                    {
                        if (check1 == true)
                        {
                            if (check2 == true)
                            {
                                foreach (var item in listOptionWarna)
                                {
                                    var productInstance = new ProductInstance();
                                    productInstance.ProductId = product.Id;
                                    productInstance.Price = product.DefaultPrice;
                                    productInstance.CreatedBy = UserName;
                                    productInstance.UpdatedBy = UserName;

                                    productInstanceRepo.Save(productInstance);

                                    var stock = stockRepo.GetByProductInstanceId(productInstance.Id);
                                    if (stock == null)
                                    {
                                        stock = new Stock();
                                        stock.CreatedBy = UserName;
                                    }
                                    stock.ProductInstanceId = productInstance.Id;
                                    stock.UpdatedBy = UserName;
                                    stock.UpdatedDate = DateTime.Now;

                                    stockRepo.Save(stock);

                                    var optionValue = optionValueRepo.GetAll().Where(i => i.OptionsId == option.Id && i.Value.ToLower().Trim() == size.ToLower().Trim()).FirstOrDefault();
                                    if (optionValue == null)
                                    {
                                        optionValue = new OptionValue();
                                        optionValue.Value = size;
                                        optionValue.OptionsId = option.Id;
                                        optionValue.CreatedBy = UserName;
                                        optionValue.UpdatedBy = UserName;
                                        optionValueRepo.Save(optionValue);
                                    }

                                    var productInstanceOption = new ProductInstanceOptions();
                                    productInstanceOption.OptionValueId = optionValue.Id;
                                    productInstanceOption.ProductInstanceId = productInstance.Id;
                                    productInstanceOptionsRepo.Save(productInstanceOption);

                                    productInstanceOption = new ProductInstanceOptions();
                                    productInstanceOption.OptionValueId = item.Id;
                                    productInstanceOption.ProductInstanceId = productInstance.Id;
                                    productInstanceOptionsRepo.Save(productInstanceOption);
                                }
                            }
                            else
                            {
                                var y = 0;
                                foreach (var item in listOptionWarna)
                                {
                                    var optionValue = optionValueRepo.GetAll().Where(i => i.OptionsId == option.Id && i.Value.ToLower().Trim() == size.ToLower().Trim()).FirstOrDefault();
                                    if (optionValue == null)
                                    {
                                        optionValue = new OptionValue();
                                        optionValue.Value = size;
                                        optionValue.OptionsId = option.Id;
                                        optionValue.CreatedBy = UserName;
                                        optionValue.UpdatedBy = UserName;
                                        optionValueRepo.Save(optionValue);
                                    }

                                    var productInstanceOption = new ProductInstanceOptions();
                                    productInstanceOption.OptionValueId = optionValue.Id;
                                    productInstanceOption.ProductInstanceId = listProductInstance[y].Id;
                                    productInstanceOptionsRepo.Save(productInstanceOption);
                                    y++;
                                }
                            }
                        }
                        else
                        {
                            var productInstance = new ProductInstance();
                            productInstance.ProductId = product.Id;
                            productInstance.Price = product.DefaultPrice;
                            productInstance.CreatedBy = UserName;
                            productInstance.UpdatedBy = UserName;

                            productInstanceRepo.Save(productInstance);

                            var stock = stockRepo.GetByProductInstanceId(productInstance.Id);
                            if (stock == null)
                            {
                                stock = new Stock();
                                stock.CreatedBy = UserName;
                            }
                            stock.ProductInstanceId = productInstance.Id;
                            stock.UpdatedBy = UserName;
                            stock.UpdatedDate = DateTime.Now;

                            stockRepo.Save(stock);

                            var optionValue = optionValueRepo.GetAll().Where(i => i.OptionsId == option.Id && i.Value.ToLower().Trim() == size.ToLower().Trim()).FirstOrDefault();
                            if (optionValue == null)
                            {
                                optionValue = new OptionValue();
                                optionValue.Value = size;
                                optionValue.OptionsId = option.Id;
                                optionValue.CreatedBy = UserName;
                                optionValue.UpdatedBy = UserName;
                                optionValueRepo.Save(optionValue);
                            }

                            var productInstanceOption = new ProductInstanceOptions();
                            productInstanceOption.OptionValueId = optionValue.Id;
                            productInstanceOption.ProductInstanceId = productInstance.Id;
                            productInstanceOptionsRepo.Save(productInstanceOption);
                        }
                    }
                }
                else
                {
                    var productInstance = new ProductInstance();
                    productInstance.ProductId = product.Id;
                    productInstance.Price = product.DefaultPrice;
                    productInstance.CreatedBy = UserName;
                    productInstance.UpdatedBy = UserName;

                    productInstanceRepo.Save(productInstance);

                    var stock = stockRepo.GetByProductInstanceId(productInstance.Id);
                    if (stock == null)
                    {
                        stock = new Stock();
                        stock.CreatedBy = UserName;
                    }
                    stock.ProductInstanceId = productInstance.Id;
                    stock.UpdatedBy = UserName;
                    stock.UpdatedDate = DateTime.Now;

                    stockRepo.Save(stock);

                    var optionValue = optionValueRepo.GetAll().Where(i => i.OptionsId == option.Id && i.Value.ToLower().Trim() == size.ToLower().Trim()).FirstOrDefault();
                    if (optionValue == null)
                    {
                        optionValue = new OptionValue();
                        optionValue.Value = size;
                        optionValue.OptionsId = option.Id;
                        optionValue.CreatedBy = UserName;
                        optionValue.UpdatedBy = UserName;
                        optionValueRepo.Save(optionValue);
                    }

                    var productInstanceOption = new ProductInstanceOptions();
                    productInstanceOption.OptionValueId = optionValue.Id;
                    productInstanceOption.ProductInstanceId = productInstance.Id;
                    productInstanceOptionsRepo.Save(productInstanceOption);

                }


                return RedirectToAction("ProductDetails", new { id = product.Id });
            }
            catch (Exception ex)
            {

                return new StatusCodeResult(500);
            }
        }

        public ActionResult ChangeOptions(long productId, long id, bool check)
        {
            try
            {
                var option = optionRepo.GetById(id);
                var product = productRepo.GetById(productId);
                if (product == null)
                {
                    return new StatusCodeResult(500);
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
                return RedirectToAction("ProductDetails", new { id = product.Id });
            }
            catch (Exception ex)
            {

                return new StatusCodeResult(500);
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

        public PartialViewResult ProductStockAndPrice(long id = 0)
        {
            var product = productRepo.GetByIdIncludeCat(id);
            var viewModel = new ProductStockPriceViewModel();
            viewModel.Product = product;
            foreach (var item in product.ProductInstance)
            {
                var productInstance = productInstanceRepo.GetByIdIncludeOptions(item.Id);
                var stock = stockRepo.GetByProductInstanceId(item.Id);
                string nama = "";
                foreach (var item1 in productInstance.ProductInstanceOptions)
                {
                    var optionValue = optionValueRepo.GetById(item1.OptionValueId);
                    nama = nama + optionValue.Value + ", ";
                }
                viewModel.ListProductInt.Add(new StockPriceModel { Stock = stock, ProductInstance = item, Nama = nama });
            }
            return PartialView(viewModel);
        }

        public ActionResult SaveProductStockAndPrice(ProductStockPriceViewModel model)
        {
            //Page untuk melihat list of product
            var product = productRepo.GetByIdIncludeCat(model.Product.Id);
            if (product == null)
            {
                product = new Product();
            }
            foreach (var item in model.ListProductInt)
            {
                var productInstance = productInstanceRepo.GetById(item.ProductInstance.Id);
                productInstance.Price = item.ProductInstance.Price;
                productInstance.UpdatedBy = UserName;
                productInstance.UpdatedDate = DateTime.Now;
                productInstanceRepo.Save(productInstance);

                var stock = stockRepo.GetById(item.Stock.Id);
                stock.Quantity = item.Stock.Quantity;
                stock.UpdatedBy = UserName;
                stock.UpdatedDate = DateTime.Now;
                stockRepo.Save(stock);
            }

            return RedirectToAction("ProductStockAndPrice", new { id = product.Id });
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

        #region Brand
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

        public ActionResult BrandDetail(long BrandId)
        {
            BrandCategoryViewModel viewmodel = new BrandCategoryViewModel();

            var parent = new List<Category>();
            parent = categoryRepo.GetAll().Where(s => s.ParentId == null).ToList();
            parent.Insert(0, new Category { Id = 0, Nama = "Choose Parent Category" });

            ViewBag.Parent = parent;

            viewmodel.Brand = brandRepo.GetById(BrandId);
            viewmodel.Categories = brAndCatRepo.GetCategoriesByBrandId(BrandId);
            
            return View(viewmodel);
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBrandCategory(long CategoryId, long BrandId)
        {
            BrandAndCategory brandcategory = new BrandAndCategory();
            brandcategory.CategoryId = CategoryId;
            brandcategory.BrandId = BrandId;

            var result = brAndCatRepo.Save(brandcategory);

            if (!result)
            {
                ViewData["Message"] = "You already add this category before !";
            }

            return RedirectToAction("BrandDetail", new { BrandId = BrandId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBrandCategory(long CategoryId, long BrandId)
        {
            BrandAndCategory brandcategory = new BrandAndCategory();
            brandcategory.CategoryId = CategoryId;
            brandcategory.BrandId = BrandId;

            brAndCatRepo.Delete(brandcategory);

            return RedirectToAction("BrandDetail", new { BrandId = BrandId });
        }

    }
}