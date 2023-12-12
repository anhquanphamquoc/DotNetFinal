using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp01.Models;
using WebApp01.Repository;

namespace WebApp01.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CheckoutController : Controller
    {
        private readonly DataContext _dataContext;
        public CheckoutController (DataContext context)
        {
            _dataContext = context;
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CheckoutModel checkout)
        {
            string ProductsName = TempData["ProductsName"] as string;

            // Kiểm tra xem TempData có tồn tại không
            if (string.IsNullOrEmpty(ProductsName))
            {
                TempData["error"] = "Không có thông tin sản phẩm";
                return RedirectToAction("Index", "Cart"); // Hoặc chuyển hướng đến một trang thông báo lỗi khác
            }

            checkout.ProductsName = ProductsName;

            // Xóa thông tin sản phẩm từ TempData để tránh giữ lại dữ liệu không cần thiết
            TempData.Remove("ProductsName");
            if (ModelState.IsValid)
            {
                _dataContext.Add(checkout);
                await _dataContext.SaveChangesAsync();
                TempData["success"] = "Đã thêm thành công";
                return RedirectToAction("Payment");
                //return View(payment);
            }
            else
            {
                TempData["error"] = "Có lỗi xảy ra";
                List<string> errors = new List<string>();
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }
        }

        public IActionResult PassDataToMail(CheckoutModel checkout)
        {
            TempData["mailContent"] = checkout.Address.ToString();
            return RedirectToAction("Contact", "Mail");
        }
    }


}
