namespace MVC.Models
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<Order> RecentOrders { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
