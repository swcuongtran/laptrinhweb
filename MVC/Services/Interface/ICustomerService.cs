using MVC.Models;

namespace MVC.Services.Interface
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(Guid id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
        Customer GetCustomerByUserId(string userId);
        IEnumerable<Customer> GetCustomersByName(string name);
    }
}
