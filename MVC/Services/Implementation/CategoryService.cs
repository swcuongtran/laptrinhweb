using MVC.Models;
using MVC.Repositories;
using MVC.Services.Interface;
namespace MVC.Services.Implementation
{
    public class CategoryService:ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllCategories() => _categoryRepository.GetAll();

        public Category GetCategoryById(int id) => _categoryRepository.GetById(id);

        public void CreateCategory(Category category)
        {
            _categoryRepository.Add(category);
            _categoryRepository.Save();
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
            _categoryRepository.Save();
        }

        public void DeleteCategory(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category != null)
            {
                _categoryRepository.Delete(category);
                _categoryRepository.Save();
            }
        }
    }
}
