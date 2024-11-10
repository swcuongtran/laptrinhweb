using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interface;
using System.Diagnostics;

namespace MVC.Controllers
{
   //Ctrler cho nút Profile ,có chức năng show thông tin người dùng
   //Nếu chưa có thông tin gì ,tức là người dùng chưa có tài khoản 
   // -> chuyển đến mục tạo tài khoản ( Phần này là của ông đó Cường )
   public class ProfileController : Controller
   {
      private readonly IUserService _userService;

      public ProfileController(IUserService userService)
      {
         _userService = userService;
      }

      public IActionResult Index()
      {
         // Lấy thông tin người dùng từ service
         var user = _userService.GetUserProfile(User.Identity.Name);  // Giả sử ta sử dụng User.Identity.Name để lấy tên người dùng hiện tại

         // Đóng gói vào ViewData hoặc ViewModel
         ViewData["User"] = user;

         return View("Profile"); //Trả về View có tên Profile 
      }
   }

}