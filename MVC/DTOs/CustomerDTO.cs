namespace MVC.DTOs
{
    public class CustomerDTO
    {
        public Guid CustomerId { get; set; }  // ID của khách hàng
        public string? UserId { get; set; }    // UserId từ hệ thống Identity
        public string Name { get; set; }      // Tên khách hàng
        public string Phone { get; set; }     // Số điện thoại
        public string Address { get; set; }   // Địa chỉ
        public string Email { get; set; }     // Email của khách hàng
    }
}
