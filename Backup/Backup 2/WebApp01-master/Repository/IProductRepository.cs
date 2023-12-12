using System.Collections.Generic;
using WebApp01.Models;

namespace WebApp01.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> GetAllProducts();
        ProductModel GetProductById(int productId);
        IEnumerable<ProductModel> SearchProductsByName(string searchTerm);
        // Thêm các phương thức khác cần thiết tại đây
    }
}
