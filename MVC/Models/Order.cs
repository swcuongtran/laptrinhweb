namespace MVC.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public enum OrderStatus
    {
        Pending = 0,        // Đang xử lý
        Shipped = 1,        // Đã giao hàng
        Delivered = 2,      // Đã giao đến người nhận
        Canceled = 3        // Đã hủy
    }
}
