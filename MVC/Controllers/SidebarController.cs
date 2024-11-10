using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interface;
using System.Diagnostics;

namespace MVC.Controllers
{
   //Tạp Ctrler cho Sidebar 
   public class SidebarController : Controller
   {
      private readonly AppDbContext _context;

      public SidebarController(AppDbContext context)
      {
         _context = context;
      }

      public IActionResult Sidebar()
      {
         var categories = _context.Categories.ToList(); // Lấy danh sách các danh mục từ cơ sở dữ liệu
         return PartialView("_Sidebar", categories);
      }
   }

}
