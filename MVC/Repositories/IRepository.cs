using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace MVC.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();  // Lấy tất cả các thực thể
        T GetById(int id);  // Lấy thực thể theo ID
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);  // Tìm kiếm thực thể theo điều kiện
        void Add(T entity);  // Thêm mới thực thể
        void Update(T entity);  // Cập nhật thực thể
        void Delete(T entity);  // Xóa thực thể
        void Save();  // Lưu các thay đổi vào cơ sở dữ liệu
    }
}
