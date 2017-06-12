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
        public ActionResult Index(long CategoryId = 0, string sort = "", decimal MinHarga = 0, decimal MaxHarga = 10000000000)
        {
            //Page product index
            var model = new ProductViewModel();
            model.ProductList = ProductList(CategoryId, sort).Where(j => j.Price >= MinHarga && j.Price <= MaxHarga).ToList();
            //model.ProductList = ProductList(CategoryId, sort).Where(j => j.Price >= MinHarga && j.Price <= MaxHarga).ToList();

            return View(model);
        }

        public PartialViewResult ProductIndex(long CategoryId = 0, string sort = "", decimal MinHarga = 0, decimal MaxHarga = 0)
        {
            //Partial view untuk refresh list of product
            var list = ProductList(CategoryId, sort).Where(j => j.Price >= MinHarga && j.Price <= MaxHarga).ToList();

            return PartialView(list);
        }

        public ActionResult Detail(long ProductId)
        {
            return View();
        }

        //Untuk Pilihan Per-Category Brand
        public IEnumerable<ProductListViewModel> ProductList(long CategoryId = 0, string sort = "")
        {
            var listProduct = new List<ProductListViewModel>();
            var productList = productRepo.GetAll().Where(j => CategoryId == 0 ? true : j.CategoryId == CategoryId).ToList();
            switch (sort)
            {
                case "Top Rating":
                    productList.OrderByDescending(j => j.Rating);
                    break;
                default:
                    break;
            }


            foreach (var item in productList)
            {
                listProduct.Add(new ProductListViewModel
                {
                    Product = item,
                    Price = productInstanceRepo.GetPriceForProductList(item.Id)
                });
            }

            return listProduct;
        }


        //Jika Product di Click akan menuju details product
        public ActionResult DetailsProduct(long ProductId = 0)
        {
            var productObj = productRepo.GetById(ProductId);


            var model = new ProductDetailsViewModel();
            model.Product = productObj;
            return View(model);
        }


        //Get Product Per-Options
        //Jadi method nya nanti akan diisi jika user milih sebuah product 
        //dengan ukuran dan warna tertentu bisa saja terjadi perubahan harga
        public JsonResult GetPriceByOptions(long ProductId = 6, string optValueWarna = "Black", string optValueUkuran = "S")
        {

            var IdProdInstance = productInstanceOptionsRepo.GetPriceByFilter(6, optValueWarna, "XS");

            var ProductInstance = productInstanceRepo.GetById(IdProdInstance);



            return Json(ProductInstance.Price);
        }
    }
}