using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp01.Models;
using WebApp01.Repository;

namespace WebApp01.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CheckoutsController : Controller
    {
        private readonly DataContext _dataContext;
        public CheckoutsController(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Checkouts.OrderByDescending(p => p.Id).ToListAsync());
        }

        [HttpPost]
        public IActionResult UpdateStatus(string checkoutId, bool isShipped)
        {
            // Thực hiện logic cập nhật trạng thái dựa trên giá trị của isShipped
            var checkout = _dataContext.Checkouts.FirstOrDefault(c => c.Id == checkoutId);

            if (checkout != null)
            {
                checkout.IsShipped = isShipped;
                _dataContext.SaveChanges();
                TempData["success"] = "Cập nhật trạng thái thành công.";
            }
            else
            {
                TempData["error"] = "Không tìm thấy đơn hàng.";
            }

            return RedirectToAction("Index"); // Chuyển hướng về trang danh sách đơn hàng
        }
        public async Task<IActionResult> Delete(int Id)
        {
            CheckoutModel checkout = await _dataContext.Checkouts.FindAsync(Id);

            if (checkout != null)
            {
                _dataContext.Checkouts.Remove(checkout);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Đã xóa đơn hàng";
            }
            else
            {
                TempData["error"] = "Không tìm thấy đơn hàng.";
            }

            return RedirectToAction("Index");
        }
    }

}