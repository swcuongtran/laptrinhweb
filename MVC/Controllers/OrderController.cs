using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interface;

namespace MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Danh sách đơn hàng
        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        // Cập nhật trạng thái đơn hàng
        [HttpPost]
        public IActionResult UpdateStatus(int id, OrderStatus status)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();

            order.Status = status;
            _orderService.UpdateOrder(order);

            return RedirectToAction("Index");
        }

        // Xóa đơn hàng
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction("Index");
        }
    }
}
