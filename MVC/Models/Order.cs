namespace MVC.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
