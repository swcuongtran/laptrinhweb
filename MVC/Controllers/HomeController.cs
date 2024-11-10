using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interface;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IOrderService _orderService;

        public HomeController(IProductService productService, ICategoryService categoryService, IOrderService orderService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProducts();
            var categories = _categoryService.GetAllCategories();

            ViewData["Products"] = products;
            ViewData["Categories"] = categories;

            return View("Home");
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Order(int productId, int quantity)
        {
            // Xử lý logic tạo đơn hàng mới
            var order = new Order
            {
                ProductId = productId,
                Quantity = quantity,
                OrderDate = DateTime.Now,
                // Các thuộc tính khác nếu có
            };

            _orderService.CreateOrder(order);

            // Chuyển hướng người dùng đến trang Lịch sử đơn hàng
            return RedirectToAction("Index", "Order");
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

//Ctrler cho nút Home ,có chức năng show đơn hàng ,danh mục sản phẩm 
    //Dự kiến bố trí thông tin theo dạng lưới ,các thông tin sẽ được gói trong 1 Container ( div ) 
    //Thông tin bao gồm : Ảnh sản phẩm ,Tên sản phẩm ,Giá tiền 
    //Khi người dùng Click vào ,nó sẽ chuyển người dùng đến 1 trang mới ,ở đây người dùng có thể order đồ uống 
    