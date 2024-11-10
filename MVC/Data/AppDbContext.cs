using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MVC.Models;
namespace MVC.Data
{
    public class AppDbContext: IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionDetail> OptionDetails { get; set; }
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

            // Bảng Bill
            modelBuilder.Entity<Bill>()
                .HasKey(b => b.BillId);  // Khóa chính
            modelBuilder.Entity<Bill>()
                .Property(b => b.BillDate)
                .IsRequired();
            modelBuilder.Entity<Bill>()
                .Property(b => b.TotalAmount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Order)  // Thiết lập khóa ngoại đến Order
                .WithMany()
                .HasForeignKey(b => b.OrderId)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa theo cascade

            // Bảng Option
            modelBuilder.Entity<Option>()
                .HasKey(o => o.OptionId);  // Khóa chính
            modelBuilder.Entity<Option>()
                .Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Option>()
                .HasIndex(o => o.Name)
                .IsUnique();  // Ràng buộc duy nhất

            // Bảng OptionDetail
            modelBuilder.Entity<OptionDetail>()
                .HasKey(od => od.OptionDetailId);  // Khóa chính
            modelBuilder.Entity<OptionDetail>()
                .Property(od => od.Value)
                .IsRequired()
                .HasMaxLength(100);  // Ràng buộc độ dài tối đa
            modelBuilder.Entity<OptionDetail>()
                .HasOne(od => od.OrderDetail)  // Khóa ngoại đến OrderDetail
                .WithMany(od => od.OptionDetails)
                .HasForeignKey(od => od.OrderDetailId)
                .OnDelete(DeleteBehavior.Cascade);  // Xóa theo cascade
            modelBuilder.Entity<OptionDetail>()
                .HasOne(od => od.Option)  // Khóa ngoại đến Option
                .WithMany()
                .HasForeignKey(od => od.OptionId)
                .OnDelete(DeleteBehavior.Restrict);  // Hạn chế xóa Option nếu có OptionDetail
            base.OnModelCreating(modelBuilder);
        }
    }
}
