using System.Collections.Generic;
using System.Linq;
using dotnet_balta.Data;
using dotnet_balta.Models;
using dotnet_balta.ViewModels.ProductViewModels;
using Microsoft.EntityFrameworkCore;

namespace dotnet_balta.Repositories
{
    public class ProductRepository
    {
        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListProductViewModel> Get()
        {
            return _context.Products
                .Include(x => x.Category)
                .Select(x => new ListProductViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Category = x.Category.Title,
                    CategoryId = x.Category.Id
                })
                .AsNoTracking()
                .ToList();
        } 
        public Product Get(int id)
        {
            return _context.Products.Find(id);
        } 
        public void Save(Product product) 
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void Update(Product product) 
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}