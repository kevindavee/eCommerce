using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eCommerce.DAL;
using eCommerce.DAL.Repositories.Brands;
using eCommerce.DAL.Repositories.The_Products.Products;
using eCommerce.Web.Models.ProductViewModels;

namespace eCommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private BrandRepo brandRepo;
        private ProductRepo productRepo;
        private ProductInstanceRepo productInstanceRepo;
        private ProductInstanceOptionsRepo productInstanceOptionsRepo;

        string userName = "";

        public ProductController(BrandRepo _brandRepo, ProductRepo _productRepo, ProductInstanceRepo _productInstanceRepo, ProductInstanceOptionsRepo _productInstanceOptionsRepo)
        {
            brandRepo = _brandRepo;
            this.productRepo = _productRepo;
            this.productInstanceRepo = _productInstanceRepo;
            this.productInstanceOptionsRepo = _productInstanceOptionsRepo;
        }
        public ActionResult Index()
        {
            //Page product index
            return View();
        }

        public ActionResult ProductIndex()
        {
            //Partial view untuk refresh list of product
            return PartialView();
        }

        public ActionResult Detail(long ProductId)
        {
            return View();
        }

        //Untuk Pilihan Per-Category Brand
        public ActionResult HomePerCategoriesView(long CategoryId = 0)
        {
            var brandList = brandRepo.GetAll().ToList();
            var productList = productRepo.GetAll().Where(j => CategoryId == 0 ? true : j.CategoryId == CategoryId).ToList();


            var model = new ProductViewModel();
            model.BrandList = brandList;
            model.ProductList = productList;
            return View();
        }


        //Jika Product di Click akan menuju details product
        public ActionResult DetailsProductView(long ProductId = 0)
        {
            var productObj = productRepo.GetById(ProductId);


            var model = new ProductDetailsViewModel();
            model.Product = productObj;
            return View();
        }


        //Get Product Per-Options
        //Jadi method nya nanti akan diisi jika user milih sebuah product 
        //dengan ukuran dan warna tertentu bisa saja terjadi perubahan harga
        public JsonResult GetPriceByOptions(long ProductId = 0, string optValueWarna = "", string optValueUkuran = "")
        {

            var IdProdInstance = productInstanceOptionsRepo.GetPriceByFilter(ProductId, optValueWarna, optValueUkuran);

            var ProductInstance = productInstanceRepo.GetById(IdProdInstance);



            return Json(ProductInstance.Price);
        }
    }
}