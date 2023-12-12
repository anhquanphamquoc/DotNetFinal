using System.Collections.Generic;
using System.Linq;
using WebApp01.Models;

namespace WebApp01.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public ProductModel GetProductById(int productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }

        public IEnumerable<ProductModel> SearchProductsByName(string searchTerm)
        {
            return _context.Products.Where(p => p.Name.Contains(searchTerm)).ToList();
        }

    }
}