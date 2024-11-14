using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        [NotMapped]
        public decimal Subtotal
        {
            get { return Quantity * UnitPrice; }
            set { } // Add setter if you need to modify it
        }

        public ICollection<OptionDetail> OptionDetails { get; set; }
    }
}
