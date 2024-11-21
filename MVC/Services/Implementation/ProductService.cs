﻿using Microsoft.EntityFrameworkCore;
using MVC.Models;
using MVC.Repositories;
using MVC.Services.Interface;
namespace MVC.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts() => _productRepository.GetAll();

        public Product GetProductById(int id) => _productRepository.GetById(id);

        public void CreateProduct(Product product)
        {
            _productRepository.Add(product);
            _productRepository.Save();
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _productRepository.Save();
        }

        public void DeleteProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Delete(product);
                _productRepository.Save();
            }
        }
        public IEnumerable<Product> SearchProducts (string searchQuery,int? categoryID)
        {
            var query = _productRepository.GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(p => p.Name.Contains(searchQuery) || p.Description.Contains(searchQuery));
            }

            if (categoryID.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryID.Value);
            }

            return query.ToList();
        }
        public IEnumerable<Product> GetFeaturedProducts(int take)
        {
            // Assuming "Rating" is a property of the Product model
            return _productRepository.GetAll()
                .Take(take)                       // Take the top 'n' products
                .ToList();
        }
        public IEnumerable<Product> GetLatestProducts(int take)
        {
            // Assuming "DateAdded" is a property of the Product model
            return _productRepository.GetAll()
                .OrderByDescending(p => p.DateAdded)  // Sort by date added in descending order
                .Take(take)                          // Take the top 'n' products
                .ToList();
        }
    }
}
