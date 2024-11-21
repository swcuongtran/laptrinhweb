using MVC.Models;
using MVC.Repositories;
using MVC.Services.Interface;
namespace MVC.Services.Implementation
{
    public class OrderService:IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> GetAllOrders() => _orderRepository.GetAllWithIncludes(o=>o.Customer);

        public Order GetOrderById(int id) => _orderRepository.GetById(id);

        public void CreateOrder(Order order)
        {
            _orderRepository.Add(order);
            _orderRepository.Save();
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
            _orderRepository.Save();
        }

        public void DeleteOrder(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order != null)
            {
                _orderRepository.Delete(order);
                _orderRepository.Save();
            }
        }
        public Order GetOrderWithDetails(int id)
        {
            var order = _orderRepository.GetAllWithIncludes(o => o.OrderDetails)
                         .FirstOrDefault(o => o.OrderId == id);
            return order;
        }
        public Order GetOrderWithCustomer(int id)
        {
            var order = _orderRepository.GetAllWithIncludes(o => o.Customer,o => o.OrderDetails,o => o.OrderDetails)
                         .FirstOrDefault(o => o.OrderId == id);
            return order;
        }
        public IEnumerable<Order> GetOrdersByCustomerId(Guid customerId)
        {
            return _orderRepository.Find(o => o.CustomerId == customerId); // Lấy tất cả đơn hàng của khách hàng theo CustomerId
        }
        public IEnumerable<Order> GetOrdersByCustomerIds(List<Guid> customerIds)
        {
            return _orderRepository.Find(o => customerIds.Contains(o.CustomerId)); // Lọc tất cả các đơn hàng của khách hàng theo danh sách customerId
        }
    }
}
