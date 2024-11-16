using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services; // Giả sử các service được định nghĩa trong namespace này
using MVC.Services.Interface;
using System.Collections.Generic;

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        // Inject các service cần thiết trong constructor
        public AdminController(IOrderService orderService, ICustomerService customerService, IProductService productService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _productService = productService;
        }

        public IActionResult Dashboard()
        {
            // Lấy danh sách đơn hàng gần đây từ order service
            var recentOrders = _orderService.GetAllOrders();

            // Lấy danh sách sản phẩm hiện có từ product service
            var products = _productService.GetAllProducts();

            // Tạo ViewModel cho Admin Dashboard
            var viewModel = new AdminDashboardViewModel
            {
                RecentOrders = recentOrders,
                Products = products
            };

            // Truyền ViewModel vào View
            return View(viewModel);
        }
        public IActionResult SearchOrders(string customerName)
        {
            // Nếu tên khách hàng không rỗng, tìm kiếm
            if (!string.IsNullOrEmpty(customerName))
            {
                var customers = _customerService.GetCustomersByName(customerName); // Lấy tất cả khách hàng có tên chứa customerName

                if (customers.Any()) // Nếu tìm thấy khách hàng
                {
                    // Lấy danh sách customerId từ các khách hàng tìm thấy
                    var customerIds = customers.Select(c => c.CustomerId).ToList();

                    // Lấy tất cả các đơn hàng của những khách hàng này
                    var orders = _orderService.GetOrdersByCustomerIds(customerIds); // Giả sử phương thức này nhận một danh sách customerId

                    return View("Dashboard", new AdminDashboardViewModel
                    {
                        RecentOrders = orders,
                        Products = _productService.GetAllProducts()
                    });
                }
                else
                {
                    TempData["ErrorMessage"] = "Không tìm thấy khách hàng với tên đã nhập.";
                }
            }

            // Nếu không tìm thấy hoặc không có tên, vẫn hiển thị dashboard bình thường
            return RedirectToAction("Dashboard");
        }
    }
}
