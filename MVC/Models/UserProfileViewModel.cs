namespace MVC.Models
{
    public class UserProfileViewModel
    {
        // Thông tin cá nhân của khách hàng
        public Customer Customer { get; set; }

        // Lịch sử đơn hàng của khách hàng
        public IEnumerable<Order> OrderHistory { get; set; }
    }
}
