using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp01.Repository;

namespace WebApp01.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        public ProductController(DataContext context)
        {
            _dataContext = context; 
        }
        public IActionResult Index(string searchTerm)
        {
            var products = from b in _dataContext.Products select b;
            //var SearchResua = await products.Where(x => EF.Functions.Like(x.Name, $"%{name}%")).ToListAsync();
            if (!String.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.Name.Contains(searchTerm)); // Change == to Contains for a partial match
            }
            var resp = products.ToList();

            // Truyền dữ liệu vào ViewBag và render view SearchResult.cshtml
            ViewBag.SearchTerm = searchTerm;
            return View("SearchResults", resp);
        }

        public async Task<IActionResult> Details(int Id ) {
            if(Id == null)
            {
                return RedirectToAction("Index");
            }
            var productById = _dataContext.Products.Where(p =>  p.Id == Id).FirstOrDefault();
            return View(productById); 
        }
	}
}
