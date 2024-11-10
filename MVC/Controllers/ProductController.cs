using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interface;

namespace MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index (string searchQuery = "", int? categoryID = null)
        {
            var product = _productService.SearchProducts(searchQuery, categoryID);
            ViewData["Categories"] = _categoryService.GetAllCategories();
            return View(product);
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
