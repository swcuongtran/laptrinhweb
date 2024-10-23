using MVC.Models;
using MVC.Repositories;
using MVC.Services.Interface;
namespace MVC.Services.Implementation
{
    public class CustomerService:ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomers() => _customerRepository.GetAll();

        public Customer GetCustomerById(int id) => _customerRepository.GetById(id);

        public void CreateCustomer(Customer customer)
        {
            _customerRepository.Add(customer);
            _customerRepository.Save();
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
            _customerRepository.Save();
        }

        public void DeleteCustomer(int id)
        {
            var customer = _customerRepository.GetById(id);
            if (customer != null)
            {
                _customerRepository.Delete(customer);
                _customerRepository.Save();
            }
        }
    }
}
