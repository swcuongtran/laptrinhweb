using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MVC.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime BillDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
