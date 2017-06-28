using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL;
using eCommerce.DAL.Repositories.Brands;
using eCommerce.DAL.Repositories.The_Products.Products;
using eCommerce.Web.Models.ProductViewModels;
using eCommerce.Core.CommerceClasses.The_Products.Products;
using Microsoft.AspNetCore.Mvc.Rendering;
using eCommerce.DAL.Repositories.The_Products.Categories;
using Microsoft.AspNetCore.Http;
using eCommerce.Core.CommerceClasses.The_Products.Reviews;
using eCommerce.DAL.Repositories.UserLogins;
using eCommerce.DAL.Repositories.The_Products.Reviews;
using eCommerce.Core.CommerceClasses.Brands;

namespace eCommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private BrandRepo brandRepo;
        private CategoryRepo categoryRepo;
        private ProductRepo productRepo;
        private ProductInstanceRepo productInstanceRepo;
        private ProductInstanceOptionsRepo productInstanceOptionsRepo;
        private ReviewRepo reviewRepo;
        private UserManagementRepo userRepo;

        private IHttpContextAccessor context;

        string userName = "";

        public ProductController(BrandRepo _brandRepo, ProductRepo _productRepo, ProductInstanceRepo _productInstanceRepo, 
                                 ProductInstanceOptionsRepo _productInstanceOptionsRepo, CategoryRepo _categoryRepo, IHttpContextAccessor _context,
                                 UserManagementRepo _userRepo, ReviewRepo _reviewRepo)
        {
            brandRepo = _brandRepo;
            this.productRepo = _productRepo;
            this.productInstanceRepo = _productInstanceRepo;
            this.productInstanceOptionsRepo = _productInstanceOptionsRepo;
            this.categoryRepo = _categoryRepo;
            context = _context;
            userRepo = _userRepo;
            reviewRepo = _reviewRepo;
        }
        public ActionResult Index(long CategoryId = 0, string sort = "", decimal MinHarga = 0, decimal MaxHarga = 10000000000, long brandId = 0)
        {
            //Page product index
            var model = new ProductViewModel();
            var productList = ProductList(CategoryId, sort).Where(j => j.Price >= MinHarga && j.Price <= MaxHarga && brandId != 0 ? j.Product.BrandId == brandId : true).ToList();
            //model.ProductList = ProductList(CategoryId, sort).Where(j => j.Price >= MinHarga && j.Price <= MaxHarga).ToList();
            var listBrand = new List<Brand>();

            foreach (var item in productList)
            {
                listBrand.Add(brandRepo.GetById(item.Product.BrandId));
            }

            
            listBrand.Insert(0, new Brand { Id = 0, Nama = "-All-"});
            model.BrandList = listBrand;
            model.CategoryId = CategoryId;
            model.ProductList.PageSize = 16;
            model.ProductList.PageIndex = 0;

            decimal TotalPage = (decimal)productList.Count / model.ProductList.PageSize;
            model.ProductList.TotalPage = (int)Math.Ceiling(TotalPage);
            model.ProductList.ProductList = productList.Skip(model.ProductList.PageIndex * model.ProductList.PageSize).Take(model.ProductList.PageSize).ToList();

            return View(model);
        }

        public PartialViewResult ProductIndex(long CategoryId = 0, string sort = "", decimal MinHarga = 0, decimal MaxHarga = 10000000000, long brandId = 0,
                                              int PageIndex = 0, int PageSize = 16)
        {
            ProductPartialPagingViewModel model = new ProductPartialPagingViewModel();
            //Partial view untuk refresh list of product
            var list = ProductList(CategoryId, sort).Where(j => j.Price >= MinHarga && j.Price <= MaxHarga && (brandId != 0 ? j.Product.BrandId == brandId : true)).ToList();

            model.PageIndex = PageIndex;
            model.PageSize = PageSize;

            decimal totalPage = (decimal)list.Count / PageSize;
            model.TotalPage = (int)Math.Ceiling(totalPage);
            model.ProductList = list.Skip(PageIndex * PageSize).Take(PageSize).ToList();

            return PartialView(model);
        }

        public ActionResult Detail(long ProductId)
        {
            return View();
        }

        //Untuk Pilihan Per-Category Brand
        public IEnumerable<ProductListViewModel> ProductList(long CategoryId = 0, string sort = "")
        {
            var listProduct = new List<ProductListViewModel>();
            //var productList = productRepo.GetAll().Where(j => CategoryId == 0 ? true : j.CategoryId == CategoryId).ToList();
            var productList = productRepo.GetByCategoryIncludeImage(CategoryId);



            foreach (var item in productList)
            {
                listProduct.Add(new ProductListViewModel
                {
                    Product = item,
                    Price = productInstanceRepo.GetPriceForProductList(item.Id),
                    PictureLocation = (item.ProductImage.Count > 0? item.ProductImage.FirstOrDefault().Path : "~/images/product6.jpg")
                });
            }

            switch (sort)
            {
                case "Top Rating":
                    listProduct = listProduct.OrderByDescending(j => j.Product.Rating).ToList();
                    break;
                case "Lowest Price":
                    listProduct = listProduct.OrderBy(j => j.Price).ToList();
                    break;
                case "Highest Price":
                    listProduct = listProduct.OrderByDescending(j => j.Price).ToList();
                    break;
                default:
                    break;
            }

            return listProduct;
        }


        //Jika Product di Click akan menuju details product
        public ActionResult DetailsProduct(long ProductId = 0)
        {
            var productObj = productRepo.GetById(ProductId);
            var productInstance = productInstanceRepo.GetAll().Where(j => j.ProductId == ProductId);
            var Category = categoryRepo.GetById((long)productObj.CategoryId);
            var ParentCategory = categoryRepo.GetById((long)Category.ParentId).Nama;
            var reviewList = reviewRepo.GetListReviewByProductId(ProductId);


            var colorList = new List<string>();
            var ukuranList = new List<string>();

            foreach (var item in productInstance)
            {
                colorList.Add(productInstanceOptionsRepo.GetWarnaByProductInstanceId(item.Id));
                if (ParentCategory != "Elektronik")
                {
                    ukuranList.Add(productInstanceOptionsRepo.GetUkuranByProductInstanceId(item.Id));
                }
            }

            colorList.Insert(0, "-Pilih Warna-");
            ukuranList.Insert(0, "-Pilih Ukuran-");


            var model = new ProductDetailsViewModel();
            model.Product = productObj;
            model.ParentCategory = ParentCategory;
            model.colorList = colorList;
            model.ukuranList = ukuranList;
            model.ReviewList = reviewList;
            //model.ProductPrice = productInstanceRepo.GetById(productInstanceOptionsRepo.GetPriceByFilter(ProductId, colorList.FirstOrDefault(), ukuranList.FirstOrDefault())).Price;


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(Review review, long ProductId)
        {
            review.CustomerId = userRepo.GetCustomerId(context.HttpContext.User.Identity.Name);
            review.TheReview = review.TheReview;
            review.ProductId = ProductId;


            reviewRepo.Save(review);

            return RedirectToAction("DetailsProduct", new { ProductId = review.ProductId });
        }

        //Get Product Per-Options
        //Jadi method nya nanti akan diisi jika user milih sebuah product 
        //dengan ukuran dan warna tertentu bisa saja terjadi perubahan harga
        public JsonResult GetPriceByOptions(long ProductId = 0, string optValueWarna = "", string optValueUkuran = "", string parentCategory = "")
        {

            var IdProdInstance = productInstanceOptionsRepo.GetPriceByFilter(ProductId, optValueWarna, optValueUkuran, parentCategory);

            var ProductInstance = productInstanceRepo.GetById(IdProdInstance);

            var result = new { Price = (ProductInstance != null ? ProductInstance.Price : 0), IdProductInstance = IdProdInstance };

            return Json(result);
        }
    }
}