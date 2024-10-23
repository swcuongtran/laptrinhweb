using MVC.Models;
using MVC.Repositories;
using MVC.Services.Interface;

namespace MVC.Services.Implementation
{
    public class OptionDetailService:IOptionDetailService
    {
        private readonly IRepository<OptionDetail> _optionDetailRepository;

        public OptionDetailService(IRepository<OptionDetail> optionDetailRepository)
        {
            _optionDetailRepository = optionDetailRepository;
        }

        public IEnumerable<OptionDetail> GetAllOptionDetails() => _optionDetailRepository.GetAll();

        public OptionDetail GetOptionDetailById(int id) => _optionDetailRepository.GetById(id);

        public void CreateOptionDetail(OptionDetail optionDetail)
        {
            _optionDetailRepository.Add(optionDetail);
            _optionDetailRepository.Save();
        }

        public void UpdateOptionDetail(OptionDetail optionDetail)
        {
            _optionDetailRepository.Update(optionDetail);
            _optionDetailRepository.Save();
        }

        public void DeleteOptionDetail(int id)
        {
            var optionDetail = _optionDetailRepository.GetById(id);
            if (optionDetail != null)
            {
                _optionDetailRepository.Delete(optionDetail);
                _optionDetailRepository.Save();
            }
        }
    }
}
