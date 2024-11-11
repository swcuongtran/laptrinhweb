using Microsoft.EntityFrameworkCore;
using MVC.Data;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace MVC.Repositories
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();  // Thiết lập DbSet cho thực thể
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.ToList();  // Lấy tất cả các thực thể
        }

        public T GetById(int id)
        {
            return _entities.Find(id);  // Tìm thực thể theo ID
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();  // Tìm kiếm với điều kiện
        }

        public void Add(T entity)
        {
            _entities.Add(entity);  // Thêm mới thực thể
        }

        public void Update(T entity)
        {
            _entities.Attach(entity);  // Gắn thực thể để cập nhật
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);  // Xóa thực thể
        }

        public void Save()
        {
            _context.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
        }
        public T GetById(Guid id)
        {
            return _context.Set<T>().Find(id); // Dùng Find để lấy thực thể theo GUID
        }
        public T FindSingle(Expression<Func<T, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }
        public IEnumerable<T> GetAllWithIncludes(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _entities;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);  // Bao gồm các mối quan hệ
            }

            return query.ToList();
        }
    }
}
