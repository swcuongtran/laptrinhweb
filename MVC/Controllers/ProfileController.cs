using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interface;
using MVC.Models;
using MVC.DTOs;

namespace MVC.Controllers
{
    public class ProfileController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;

        public ProfileController(UserManager<IdentityUser> userManager, ICustomerService customerService, IOrderService orderService)
        {
            _userManager = userManager;
            _customerService = customerService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var customer = _customerService.GetCustomerByUserId(userId);
            if (customer == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng.");
            }

            var orderHistory = _orderService.GetOrdersByCustomerId(customer.CustomerId);

            var customerDTO = new CustomerDTO
            {
                Name = customer.Name,
                Phone = customer.Phone,
                Address = customer.Address
            };
            var model = new UserProfileDTO
            {
                Customer = customerDTO,
                OrderHistory = orderHistory
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = _userManager.GetUserId(User);  // Lấy UserId từ hệ thống Identity
            var customer = _customerService.GetCustomerByUserId(userId);

            if (customer == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng.");
            }

            var orderHistory = _orderService.GetOrdersByCustomerId(customer.CustomerId);

            var model = new UserProfileDTO
            {
                Customer = new CustomerDTO
                {
                    CustomerId = customer.CustomerId,
                    UserId = customer.UserId,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Address = customer.Address,
                    Email = customer.Email // Thêm Email
                },
                OrderHistory = orderHistory
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserProfileDTO model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);  // Lấy UserId từ hệ thống Identity
                var customer = _customerService.GetCustomerByUserId(userId);

                if (customer == null)
                {
                    return NotFound("Không tìm thấy thông tin người dùng.");
                }

                // Cập nhật thông tin khách hàng từ DTO
                customer.Name = model.Customer.Name;
                customer.Phone = model.Customer.Phone;
                customer.Address = model.Customer.Address;

                // Lưu thay đổi vào database
                _customerService.UpdateCustomer(customer);

                return RedirectToAction("Index");  // Quay lại trang hồ sơ người dùng
            }

            return View(model);
        }
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("Bạn cần đăng nhập để xem chi tiết đơn hàng.");
            }

            // Lấy thông tin khách hàng từ userId
            var customer = _customerService.GetCustomerByUserId(userId);
            if (customer == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng.");
            }

            // Lấy chi tiết đơn hàng
            var order = _orderService.GetOrderWithDetails(orderId);
            if (order == null || order.CustomerId != customer.CustomerId)
            {
                return NotFound("Không tìm thấy đơn hàng hoặc bạn không có quyền truy cập.");
            }

            return View(order);  // Trả về view hiển thị chi tiết đơn hàng
        }

    }
}
