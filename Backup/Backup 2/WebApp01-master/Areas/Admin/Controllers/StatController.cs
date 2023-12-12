using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Data;
using WebApp01.Models;
using WebApp01.Repository;

namespace WebApp01.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StatController : Controller
    {
        private readonly UserManager<AppUserModel> _userManager;
        private readonly DataContext _dataContext;

        public StatController(UserManager<AppUserModel> userManager, DataContext dataContext)
        {
            _userManager = userManager;
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            var totalCustomers = _userManager.Users.Count(); // Đếm số lượng tài khoản
            var totalOrders = _dataContext.Checkouts.Count(); // Đếm số lượng đơn hàng
            var totalBooks = _dataContext.Products.Count(); // Đếm số lượng sách

            ViewBag.TotalCustomers = totalCustomers;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.TotalBooks = totalBooks;

            return View();
        }
    }
}
