﻿using MVC.Models;
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

        public IEnumerable<Order> GetAllOrders() => _orderRepository.GetAll();

        public Order GetOrderById(Guid id) => _orderRepository.GetById(id);

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
        public IEnumerable<Order> GetOrdersByCustomerId(Guid customerId)
        {
            return _orderRepository.Find(o => o.CustomerId == customerId); // Lấy tất cả đơn hàng của khách hàng theo CustomerId
        }
    }
}
