using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interface;
using System.Diagnostics;

namespace MVC.Controllers
{
   //Chưa có gì trong này hết ,nó sẽ là nút sẵn sàng cho việc mở rộng web 
   public class SettingController : Controller
   {
      public IActionResult Index()
      {
         // Hiển thị trang cài đặt (tạm thời để trống)
         return View("Settings");
      }
   }

}