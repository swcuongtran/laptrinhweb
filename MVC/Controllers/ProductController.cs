using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Models;
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

            if (User.IsInRole("Admin"))
            {
                // Admin thấy danh sách sản phẩm chi tiết
                return View("AdminIndex", product);
            }
            else
            {
                // User thấy danh sách sản phẩm với thông tin cơ bản
                return View("UserIndex", product);
            }
        }

        public IActionResult Details(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();
            if (User.IsInRole("Admin"))
            {
                return View("AdminDetails", product);
            }
            else
            {
                // Hiển thị View dành cho User
                return View("UserDetails", product);
            }
        }
       public IActionResult Create()
        {
            var categories = _categoryService.GetAllCategories();

            // Truyền danh sách vào ViewData
            ViewData["Categories"] = categories;

            // Trả về View với một đối tượng Product rỗng
            return View(new Product());
        }
        // POST: Tạo mới sản phẩm
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.CreateProduct(product);
                TempData["SuccessMessage"] = "Product created successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create product. Please check the inputs.";
                return RedirectToAction("Index");
            }
        }
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound();

            ViewData["Categories"] = _categoryService.GetAllCategories()
            .Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name,
                Selected = (c.CategoryId == product.CategoryId) // chọn mặc định nếu đúng ID
            }).ToList();

            return View(product);
        }

        // POST: Cập nhật sản phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductId) return BadRequest();

            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categories"] = _categoryService.GetAllCategories();
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var product = _productService.GetProductById(id);
            if (product != null)
            {
                _productService.DeleteProduct(id);
                TempData["SuccessMessage"] = "Product deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Product not found.";
            }
            return RedirectToAction("Index");
        }
    }
}
