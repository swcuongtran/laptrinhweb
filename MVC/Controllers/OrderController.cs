using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interface;
using System.Diagnostics;

namespace MVC.Controllers
{
    // Tạo Ctrler cho nút Order, có chức năng như lịch sử đơn hàng, bao gồm các món đã, đang và sẽ mua
    // Dự kiến sẽ được bố trí theo dạng List, chỉ khi nào người dùng mua hàng thì mới có thông tin
    // Nếu chưa mua thì trang sẽ chỉ hiển thị: Chưa có đơn hàng nào, đặt hàng ngay thôi!
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            // Lấy danh sách đơn hàng từ service
            var orders = _orderService.GetAllOrders();
            // Đóng gói vào ViewData hoặc ViewModel
            ViewData["Orders"] = orders;
            // Trả về View có tên là Order.cshtml
            return View("Order");
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

            // Chuyển hướng người dùng đến trang xác nhận đơn hàng hoặc trang khác
            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            // Lấy thông tin đơn hàng vừa tạo
            var order = _orderService.GetOrderById(orderId);
            return View(order);
        }
    }
}
