using MVC.Models;

namespace MVC.Services.Interface
{
    public interface IOptionService
    {
        IEnumerable<Option> GetAllOptions();
        Option GetOptionById(int id);
        void CreateOption(Option option);
        void UpdateOption(Option option);
        void DeleteOption(int id);
    }
}
