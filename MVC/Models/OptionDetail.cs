namespace MVC.Models
{
    public class OptionDetail
    {
        public int OptionDetailId { get; set; }
        public int OrderDetailId { get; set; }
        public OrderDetail OrderDetail { get; set; }
        public int OptionId {  get; set; }
        public Option Option { get; set; }
        public string Value { get; set; }
    }
}
