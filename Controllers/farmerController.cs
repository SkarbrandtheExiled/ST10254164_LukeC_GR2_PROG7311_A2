using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            // Check if user is logged in and is an employee
            if (HttpContext.Session.GetString("Role") != "employee")
            {
                return RedirectToAction("LoginView", "Account");
            }

            // Get all farmers for the dropdown
            ViewBag.Farmers = _context.products.Select(p => p.farmerName).Distinct().ToList();

            // Get all categories for the dropdown
            ViewBag.Categories = _context.products.Select(p => p.Category).Distinct().ToList();

            // Start with all products
            var productsQuery = _context.products.AsQueryable();

            // Apply filters if provided
            if (!string.IsNullOrEmpty(farmerFilter))
            {
                productsQuery = productsQuery.Where(p => p.farmerName == farmerFilter);
            }

            if (!string.IsNullOrEmpty(categoryFilter))
            {
                productsQuery = productsQuery.Where(p => p.Category == categoryFilter);
            }

            if (dateFrom.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.productCreationDate >= dateFrom.Value);
            }

            if (dateTo.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.productCreationDate <= dateTo.Value);
            }

            // Execute query and get products
            var products = productsQuery.ToList();

            // Store the current filter values for form
            ViewBag.CurrentFarmerFilter = farmerFilter;
            ViewBag.CurrentCategoryFilter = categoryFilter;
            ViewBag.CurrentDateFrom = dateFrom;
            ViewBag.CurrentDateTo = dateTo;

            return View(products);
        }
    }
}
