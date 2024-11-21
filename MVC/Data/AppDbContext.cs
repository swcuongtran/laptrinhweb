using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MVC.Models;
namespace MVC.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Bảng Category
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);  // Khóa chính
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()  // Không cho phép null
                .HasMaxLength(100);  // Giới hạn độ dài
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();  // Ràng buộc duy nhất

            // Bảng Product
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);  // Khóa chính
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()  // Không cho phép null
                .HasMaxLength(100);  // Giới hạn độ dài
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");  // Định dạng kiểu decimal
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)  // Thiết lập khóa ngoại đến Category
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa theo cascade

            // Bảng Customer
            modelBuilder.Entity<Customer>()
                .HasKey(cu => cu.CustomerId);  // Khóa chính
            modelBuilder.Entity<Customer>()
                .Property(cu => cu.Name)
                .IsRequired()  // Không cho phép null
                .HasMaxLength(100);  // Giới hạn độ dài
            modelBuilder.Entity<Customer>()
                .Property(cu => cu.Phone)
                .IsRequired()
                .HasMaxLength(15);
            modelBuilder.Entity<Customer>()
                .Property(cu => cu.Email)
                .HasMaxLength(100);
            modelBuilder.Entity<Customer>()
                .HasIndex(cu => cu.Email)
                .IsUnique();  // Ràng buộc duy nhất (unique)

            // Bảng Order
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);  // Khóa chính
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .IsRequired();  // Không cho phép null
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)  // Thiết lập khóa ngoại đến Customer
                .WithMany(cu => cu.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa theo cascade

            // Bảng OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.OrderDetailId);  // Khóa chính
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Quantity)
                .IsRequired();
            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.Subtotal)
                .HasColumnType("decimal(18,2)");  // Định dạng kiểu decimal
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)  // Thiết lập khóa ngoại đến Order
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa theo cascade
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)  // Thiết lập khóa ngoại đến Product
                .WithMany()
                .HasForeignKey(od => od.ProductId);






            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
        .HasOne(c => c.User) // Liên kết với IdentityUser
        .WithOne()
        .HasForeignKey<Customer>(c => c.UserId)
        .IsRequired();
        }
    }
}
