using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class farmerController : Controller
    {
        private readonly applicationDBContext _context;
        public farmerController(applicationDBContext context)
        {
            _context = context;
        }
        public IActionResult farmerDashboard()
        {
            // Check if user is logged in and is a farmer
            if (HttpContext.Session.GetString("Role") != "farmer")
            {
                return RedirectToAction("LoginView", "Account");
            }

            return View();
        }

        [HttpGet]
        public IActionResult addProductView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addProductView(productModel model)
        {
            if (ModelState.IsValid)
            {
                model.dateAdded = DateTime.Now; 
                _context.products.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("viewProducts");
            }
            return View(model);
        }

        public IActionResult viewProducts(string farmerFilter = "", string categoryFilter = "", DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            // Check if user is logged in and is a farmer
            if (HttpContext.Session.GetString("Role") != "farmer")
            {
                return RedirectToAction("LoginView", "Account");
            }
            // Get current farmer's username
            string currentFarmer = HttpContext.Session.GetString("User");

            // Filter products to only show current farmer's products
            var products = _context.products
                .Where(p => p.farmerName == currentFarmer)
                .ToList();

            // Get categories for this farmer only (for filtering)
            ViewBag.Categories = _context.products
                .Where(p => p.farmerName == currentFarmer)
                .Select(p => p.Category)
                .Distinct()
                .ToList();

            return View(products);
        }
    }
}
