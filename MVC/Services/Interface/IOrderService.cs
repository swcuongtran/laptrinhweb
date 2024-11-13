using MVC.Models;

namespace MVC.Services.Interface
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(Guid id);
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int id);
        Order GetOrderWithDetails(int id);
        IEnumerable<Order> GetOrdersByCustomerId(Guid customerId);
    }
}
