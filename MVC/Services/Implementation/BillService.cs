using MVC.Models;
using MVC.Repositories;
using MVC.Services.Interface;
namespace MVC.Services.Implementation
{
    public class BillService:IBillService
    {
        private readonly IRepository<Bill> _billRepository;

        public BillService(IRepository<Bill> billRepository)
        {
            _billRepository = billRepository;
        }

        public IEnumerable<Bill> GetAllBills() => _billRepository.GetAll();

        public Bill GetBillById(int id) => _billRepository.GetById(id);

        public void CreateBill(Bill bill)
        {
            _billRepository.Add(bill);
            _billRepository.Save();
        }

        public void UpdateBill(Bill bill)
        {
            _billRepository.Update(bill);
            _billRepository.Save();
        }

        public void DeleteBill(int id)
        {
            var bill = _billRepository.GetById(id);
            if (bill != null)
            {
                _billRepository.Delete(bill);
                _billRepository.Save();
            }
        }
    }
}
