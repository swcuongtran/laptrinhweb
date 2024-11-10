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

        public CheckoutController(ICartService cartService, IOrderService orderService, UserManager<IdentityUser> userManager)
        {
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
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
            if (userId == null)
            {
                // Nếu người dùng chưa đăng nhập, chuyển hướng họ đến trang đăng nhập.
                return RedirectToAction("Login", "Account");
            }

            var cartItem = _cartService.GetCartItems();

            if (!cartItem.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn đang trống");
                return RedirectToAction("Index", "Cart");
            }
            var order = new Order
            {
                CustomerId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = cartItem.Sum(i => i.Total),
                Status = "Pending",
                OrderDetails = cartItem.Select(i => new OrderDetail
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.Price
                }).ToList()
            };
            _orderService.CreateOrder(order);
            _cartService.ClearCart();
            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }
        public IActionResult OrderConfirmation(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

    }
}

