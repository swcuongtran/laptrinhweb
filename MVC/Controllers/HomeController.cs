using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interface;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index(string search = "", int? categoryId = null)
        {

            var categories = _categoryService.GetAllCategories();

            // Lấy các sản phẩm theo tìm kiếm và lọc theo categoryId
            var products = _productService.SearchProducts(search, categoryId);

            // Lấy 4 sản phẩm nổi bật từ cơ sở dữ liệu
            var featuredProducts = _productService.GetFeaturedProducts(4);
            var latestProducts = _productService.GetLatestProducts(8);
            ViewData["Products"] = products;
            ViewData["Categories"] = categories;
            ViewData["SearchQuery"] = search;
            ViewData["CategoryID"] = categoryId;

            // Truyền danh sách sản phẩm nổi bật và mới nhất
            ViewData["FeaturedProducts"] = featuredProducts;
            ViewData["LatestProducts"] = latestProducts;
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
