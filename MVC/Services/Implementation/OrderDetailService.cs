using MVC.Models;
using MVC.Repositories;
using MVC.Services.Interface;
namespace MVC.Services.Implementation
{
    public class OrderDetailService:IOrderDetailService
    {
        private readonly IRepository<OrderDetail> _orderDetailRepository;

        public OrderDetailService(IRepository<OrderDetail> orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public IEnumerable<OrderDetail> GetAllOrderDetails() => _orderDetailRepository.GetAll();

        public OrderDetail GetOrderDetailById(int id) => _orderDetailRepository.GetById(id);

        public void CreateOrderDetail(OrderDetail orderDetail)
        {
            _orderDetailRepository.Add(orderDetail);
            _orderDetailRepository.Save();
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _orderDetailRepository.Update(orderDetail);
            _orderDetailRepository.Save();
        }

        public void DeleteOrderDetail(int id)
        {
            var orderDetail = _orderDetailRepository.GetById(id);
            if (orderDetail != null)
            {
                _orderDetailRepository.Delete(orderDetail);
                _orderDetailRepository.Save();
            }
        }
    }
}
