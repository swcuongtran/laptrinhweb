using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.Repositories;

namespace MVC.Services.Implementation
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.Find(id);
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            return _context.Products.Where(predicate).ToList();
        }

        public void Add(Product entity)
        {
            _context.Products.Add(entity);
        }

        public void Update(Product entity)
        {
            _context.Products.Update(entity);
        }

        public void Delete(Product entity)
        {
            _context.Products.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
