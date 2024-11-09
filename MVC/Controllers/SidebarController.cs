using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interface;
using System.Diagnostics;

namespace MVC.Controllers
{
   public class SidebarController : Controller
   {
      private readonly AppDbContext _context;

      public SidebarController(AppDbContext context)
      {
         _context = context;
      }

      public IActionResult Sidebar()
      {
         // Ví dụ lấy danh sách Categories từ cơ sở dữ liệu để hiển thị trên Sidebar
         var categories = _context.Categories.Select(c => new SidebarItem
         {
               Name = c.Name,
               Icon = "fas fa-coffee", // Icon có thể thay đổi tùy theo loại dữ liệu
               Action = Url.Action("Index", "Category", new { id = c.CategoryId })
         }).ToList();

         // Thêm các mục tĩnh khác vào Sidebar (nếu cần)
         categories.Add(new SidebarItem { Name = "Orders", Icon = "fas fa-receipt", Action = Url.Action("Index", "Order") });
         categories.Add(new SidebarItem { Name = "Customers", Icon = "fas fa-users", Action = Url.Action("Index", "Customer") });

         return PartialView("_Sidebar", categories);
      }
   }
}