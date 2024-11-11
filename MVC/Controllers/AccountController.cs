using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Implementation;
using MVC.Services.Interface;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICustomerService _customerService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ICustomerService customerService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerService = customerService;
        }
        //register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var customer = new Customer
                    {
                        CustomerId = Guid.NewGuid(),
                        UserId = user.Id,
                        Name = model.Username,
                        Email = model.Email,
                        Address = model.Address,
                        Phone = string.IsNullOrEmpty(model.Phone) ? "Không có số điện thoại" : model.Phone
                    };
                    _customerService.CreateCustomer(customer);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    
                    ModelState.AddModelError("", error.Description);
                }
                
            }
            return View(model);
        }
        //login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(model.Email);
                }
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                ModelState.AddModelError(string.Empty, "Đăng nhập không thành công. Vui lòng kiểm tra lại thông tin.");
            }
            return View(model);
        }
        //logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
