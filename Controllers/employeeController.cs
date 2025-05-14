using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class employeeController : Controller
    {
        private readonly applicationDBContext _context;

        public employeeController(applicationDBContext context)
        {
            _context = context;
        }

        public IActionResult employeeDashboard()
        {
            // Check for employee role
            if (HttpContext.Session.GetString("UserRole") != "Employee")
            {
                return RedirectToAction("LoginView", "Account"); // Redirect if not an employee
            }
            return View();
        }

        public IActionResult AddFarmerView()
        {
            // Check for employee role
            if (HttpContext.Session.GetString("UserRole") != "Employee")
            {
                return RedirectToAction("LoginView", "Account");
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddFarmerView(farmerModel model)
        {
            // Check for employee role
            if (HttpContext.Session.GetString("UserRole") != "Employee")
            {
                return RedirectToAction("LoginView", "Account");
            }

            if (ModelState.IsValid)
            {
                // Ensure the email is unique
                if (_context.farmers.Any(f => f.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email address is already taken.");
                    return View(model);
                }
                model.Password = model.Password;
                _context.farmers.Add(model);
                _context.SaveChanges();
                return RedirectToAction("employeeDashboard");
            }
            return View(model);
        }

        public IActionResult ViewAllProducts(string farmerFilter, string categoryFilter, DateTime? dateFrom, DateTime? dateTo)
        {
            // Check for employee role
            if (HttpContext.Session.GetString("UserRole") != "Employee")
            {
                return RedirectToAction("LoginView", "Account");
            }

            var products = _context.products.AsQueryable();

            if (!string.IsNullOrEmpty(farmerFilter))
            {
                products = products.Where(p => p.farmerName == farmerFilter);
            }

            if (!string.IsNullOrEmpty(categoryFilter))
            {
                products = products.Where(p => p.Category == categoryFilter);
            }

            if (dateFrom.HasValue)
            {
                products = products.Where(p => p.dateAdded >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                products = products.Where(p => p.dateAdded <= dateTo.Value);
            }

            // Use .Select to get distinct farmer names and categories
            ViewBag.Farmers = _context.products.Select(p => p.farmerName).Distinct().OrderBy(n => n).ToList();
            ViewBag.Categories = _context.products.Select(p => p.Category).Distinct().OrderBy(c => c).ToList();
            ViewBag.CurrentFarmerFilter = farmerFilter;
            ViewBag.CurrentCategoryFilter = categoryFilter;
            ViewBag.CurrentDateFrom = dateFrom;
            ViewBag.CurrentDateTo = dateTo;

            return View(products.ToList());
        }
    }
}