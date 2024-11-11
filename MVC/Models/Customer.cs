using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models
{
    public class Customer
    {
        
        public Guid CustomerId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public ICollection<Order> Orders { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
