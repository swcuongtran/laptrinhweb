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

            
            var products = _productService.SearchProducts(search, categoryId);

            
            ViewData["Products"] = products;
            ViewData["Categories"] = categories;
            ViewData["SearchQuery"] = search; 
            ViewData["CategoryID"] = categoryId;

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
