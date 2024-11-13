using MVC.Models;

namespace MVC.DTOs
{
    public class UserProfileDTO
    {
        public CustomerDTO Customer { get; set; }
        public IEnumerable<Order>? OrderHistory { get; set; }
    }
}
