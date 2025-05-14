using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class farmerController : Controller
    {
        private readonly applicationDBContext _context;

        public farmerController(applicationDBContext context) // Inject DbContext
        {
            _context = context;
        }

        public IActionResult farmerDashboard()
        {
            if (HttpContext.Session.GetString("Role") != "farmer")
                return RedirectToAction("loginView", "account");
            return View();
        }

        [HttpGet]
        public IActionResult addProductView()
        {
            if (HttpContext.Session.GetString("Role") != "farmer")
                return RedirectToAction("loginView", "account");
            return View(new productModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addProductView(productModel model)
        {
            if (HttpContext.Session.GetString("Role") != "farmer")
                return RedirectToAction("loginView", "account");

            var farmerNameFromSession = HttpContext.Session.GetString("FarmerName");
            var farmerIdFromSession = HttpContext.Session.GetString("FarmerID");

            if (string.IsNullOrEmpty(farmerNameFromSession) || string.IsNullOrEmpty(farmerIdFromSession))
            {
                TempData["ErrorMessage"] = "Session expired or farmer information is missing. Please login again.";
                return RedirectToAction("loginView", "account");
            }

            if (!int.TryParse(farmerIdFromSession, out int farmerId))
            {
                TempData["ErrorMessage"] = "Invalid farmer ID in session. Please login again.";
                return RedirectToAction("loginView", "account");
            }

            // Explicitly check if the farmer exists with this ID, good for data integrity
            var farmerExists = await _context.farmers.AnyAsync(f => f.farmerID == farmerId && f.farmerName == farmerNameFromSession);
            if (!farmerExists)
            {
                TempData["ErrorMessage"] = "Farmer associated with session not found. Please login again.";
                return RedirectToAction("loginView", "account");
            }

            model.farmerID = farmerId; // Set the foreign key
            model.farmerName = farmerNameFromSession; // Also set the farmerName string property
            model.dateAdded = DateTime.Now;
            // model.productCreationDate is bound from the form

            if (ModelState.IsValid)
            {
                try
                {
                    _context.products.Add(model);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Product added successfully!";
                    return RedirectToAction("viewProducts");
                }
                catch (DbUpdateException ex) // More specific exception
                {
                    // Log the exception (ex.InnerException?.Message or ex.ToString())
                    ModelState.AddModelError("", "An error occurred while saving the product to the database. Check console/logs for details.");
                }
                catch (Exception ex)
                {
                    // Log the exception (ex.ToString())
                    ModelState.AddModelError("", "An unexpected error occurred while adding the product.");
                }
            }

            return View(model); // Return view with model and validation errors
        }

        public async Task<IActionResult> viewProducts()
        {
            if (HttpContext.Session.GetString("Role") != "farmer")
                return RedirectToAction("loginView", "account");

            var farmerIdFromSession = HttpContext.Session.GetString("FarmerID");
            if (string.IsNullOrEmpty(farmerIdFromSession) || !int.TryParse(farmerIdFromSession, out int farmerId))
            {
                TempData["ErrorMessage"] = "Session expired or farmer information is missing. Please login again.";
                return RedirectToAction("loginView", "account");
            }

            var products = await _context.products.Where(p => p.farmerID == farmerId).ToListAsync();

            // For categories in dropdown (if needed for filtering on this page by the farmer themselves)
            ViewBag.Categories = await _context.products.Where(p => p.farmerID == farmerId).Select(p => p.Category).Distinct().ToListAsync();
            return View(products);
        }
    }
}