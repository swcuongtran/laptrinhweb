using MVC.Models;
using MVC.Repositories;
using MVC.Services.Interface;
namespace MVC.Services.Implementation
{
    public class OptionService:IOptionService
    {
        private readonly IRepository<Option> _optionRepository;

        public OptionService(IRepository<Option> optionRepository)
        {
            _optionRepository = optionRepository;
        }

        public IEnumerable<Option> GetAllOptions() => _optionRepository.GetAll();

        public Option GetOptionById(int id) => _optionRepository.GetById(id);

        public void CreateOption(Option option)
        {
            _optionRepository.Add(option);
            _optionRepository.Save();
        }

        public void UpdateOption(Option option)
        {
            _optionRepository.Update(option);
            _optionRepository.Save();
        }

        public void DeleteOption(int id)
        {
            var option = _optionRepository.GetById(id);
            if (option != null)
            {
                _optionRepository.Delete(option);
                _optionRepository.Save();
            }
        }
    }
}
