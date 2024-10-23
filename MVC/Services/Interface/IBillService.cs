using MVC.Models;

namespace MVC.Services.Interface
{
    public interface IBillService
    {
        IEnumerable<Bill> GetAllBills();
        Bill GetBillById(int id);
        void CreateBill(Bill bill);
        void UpdateBill(Bill bill);
        void DeleteBill(int id);
    }
}
