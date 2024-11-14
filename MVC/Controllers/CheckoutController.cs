using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interface;

namespace MVC.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICustomerService _customerService;
        private readonly IOrderDetailService _orderDetailService;

        public CheckoutController(ICartService cartService, IOrderService orderService, UserManager<IdentityUser> userManager, ICustomerService customerService, IOrderDetailService orderDetailService)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
            _customerService = customerService;
            _orderDetailService = orderDetailService;
        }

        public IActionResult Index()
        {
            var cartItem = _cartService.GetCartItems();
            if (!cartItem.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống");
                return RedirectToAction("Index", "Cart");
            }
            ViewBag.CartTotal = _cartService.GetCartTotal();
            return View(cartItem);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = _userManager.GetUserId(User);

            // Kiểm tra xem userId có null hay không
            if (userId == null)
            {
                // Nếu người dùng chưa đăng nhập, chuyển hướng họ đến trang đăng nhập.
                return RedirectToAction("Login", "Account");
            }

            // Kiểm tra xem userId có phải là GUID hợp lệ không
            if (!Guid.TryParse(userId, out var customerGuid))
            {
                ModelState.AddModelError("", "User ID không hợp lệ.");
                return RedirectToAction("Index", "Home");
            }

            var cartItem = _cartService.GetCartItems();

            if (!cartItem.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống");
                return RedirectToAction("Index", "Cart");
            }
            // Lấy Customer từ bảng Customers dựa trên UserId
            var customer = _customerService.GetCustomerByUserId(userId); // Lấy Customer bằng UserId
            if (customer == null)
            {
                ModelState.AddModelError("", "Khách hàng không tồn tại. Vui lòng đăng ký tài khoản.");
                return RedirectToAction("Register", "Account");
            }
            var order = new Order
            {
                CustomerId = customer.CustomerId, // Sử dụng customerGuid (GUID hợp lệ)
                OrderDate = DateTime.Now,
                TotalAmount = cartItem.Sum(i => i.Total),
                Status = OrderStatus.Pending,
            };

            // Lưu đơn hàng vào database
            _orderService.CreateOrder(order);

            // Lưu OrderDetails cho đơn hàng
            foreach (var cart in cartItem)
            {
                var orderDetail = new OrderDetail
                {
                    ProductName = cart.ProductName,
                    OrderId = order.OrderId, // Đảm bảo OrderId của đơn hàng đã được lưu vào Order trước
                    ProductId = cart.ProductId,
                    Quantity = cart.Quantity,
                    UnitPrice = cart.Price,
                    Subtotal = cart.Quantity * cart.Price
                };

                // Lưu OrderDetail vào cơ sở dữ liệu
                _orderDetailService.CreateOrderDetail(orderDetail);
            }

            // Xóa các sản phẩm trong giỏ hàng
            _cartService.ClearCart();

            // Chuyển hướng đến trang xác nhận đơn hàng
            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            // Lấy thông tin đơn hàng từ database
            var order = _orderService.GetOrderWithDetails(orderId);

            // Kiểm tra nếu không tìm thấy đơn hàng
            if (order == null)
            {
                return NotFound();
            }

            // Trả về View hiển thị thông tin đơn hàng
            return View(order);
        }

    }
}

