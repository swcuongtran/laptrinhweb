using MVC.Models;

namespace MVC.Services.Interface
{
    public interface IOptionDetailService
    {
        IEnumerable<OptionDetail> GetAllOptionDetails();
        OptionDetail GetOptionDetailById(int id);
        void CreateOptionDetail(OptionDetail optionDetail);
        void UpdateOptionDetail(OptionDetail optionDetail);
        void DeleteOptionDetail(int id);
    }
}
