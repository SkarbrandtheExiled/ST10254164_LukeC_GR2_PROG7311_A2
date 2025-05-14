using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class employeeController : Controller
    {
        private readonly applicationDBContext _context;

        public employeeController(applicationDBContext context) // Inject DbContext
        {
            _context = context;
        }

        private string HashPassword(string password)
        {
            return password; // Placeholder!
        }
        public IActionResult employeeDashboard()
        {
            if (HttpContext.Session.GetString("Role") != "employee")
                return RedirectToAction("loginView", "account");
            return View();
        }

        [HttpGet]
        public IActionResult addFarmerView()
        {
            if (HttpContext.Session.GetString("Role") != "employee")
                return RedirectToAction("loginView", "account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addFarmerView(string farmerName, string email, string password)
        {
            if (HttpContext.Session.GetString("Role") != "employee")
                return RedirectToAction("loginView", "account");

            if (string.IsNullOrWhiteSpace(farmerName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Message = "Farmer name, email, and password are required.";
                return View();
            }

            bool farmerExists = await _context.farmers.AnyAsync(f => f.farmerName == farmerName || f.Email == email);
            if (farmerExists)
            {
                ViewBag.Message = "A farmer with that name or email already exists.";
                return View();
            }

            var newFarmer = new farmerModel
            {
                farmerName = farmerName,
                Email = email,
Password = password
            };

            try
            {
                _context.farmers.Add(newFarmer);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Farmer added successfully!";
                return RedirectToAction("employeeDashboard");
            }
            catch (DbUpdateException ex)
            {
                // Log ex
                ViewBag.Message = "An error occurred while saving the farmer. The name or email might already exist.";
                return View();
            }
            catch (Exception ex)
            {
                // Log ex
                ViewBag.Message = "An unexpected error occurred while adding the farmer.";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> viewAllProducts(string farmerFilter = "", string categoryFilter = "", DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            if (HttpContext.Session.GetString("Role") != "employee")
                return RedirectToAction("loginView", "account");

            IQueryable<productModel> query = _context.products.AsQueryable();

            if (!string.IsNullOrEmpty(farmerFilter))
            {
                query = query.Where(p => p.farmerName == farmerFilter); // Assumes productModel.farmerName is populated
            }
            if (!string.IsNullOrEmpty(categoryFilter))
            {
                query = query.Where(p => p.Category == categoryFilter);
            }
            if (dateFrom.HasValue)
            {
                query = query.Where(p => p.productCreationDate >= dateFrom.Value);
            }
            if (dateTo.HasValue)
            {
                // Add 1 day and use LessThan to include the whole 'dateTo' day
                query = query.Where(p => p.productCreationDate < dateTo.Value.AddDays(1));
            }

            var products = await query.ToListAsync();

            ViewBag.Farmers = await _context.farmers.Select(f => f.farmerName).Distinct().ToListAsync();
            ViewBag.Categories = await _context.products.Select(p => p.Category).Distinct().ToListAsync();

            ViewBag.CurrentFarmerFilter = farmerFilter;
            ViewBag.CurrentCategoryFilter = categoryFilter;
            ViewBag.CurrentDateFrom = dateFrom;
            ViewBag.CurrentDateTo = dateTo;

            return View(products);
        }
    }
}