using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            // Check for farmer role
            if (HttpContext.Session.GetString("UserRole") != "Farmer")
            {
                return RedirectToAction("LoginView", "Account");
            }
            return View();
        }

        public IActionResult ViewProducts()
        {
            //Checks for farmer role when viewing products
            if (HttpContext.Session.GetString("UserRole") != "Farmer")
            {
                return RedirectToAction("LoginView", "Account");
            }

            //Gets the farmer's ID from the session
            int farmerId = HttpContext.Session.GetInt32("UserId").Value;

            var products = _context.products
                .Where(p => p.Farmer.FarmerId == farmerId)
                .ToList();
            return View(products);
        }

        public IActionResult AddProductView()
        {
            //Checks for farmer role when adding a product
            if (HttpContext.Session.GetString("UserRole") != "Farmer")
            {
                return RedirectToAction("LoginView", "Account");
            }
            return View(new productModel());
        }

        [HttpPost]
        public IActionResult AddProductView(productModel model)
        {
           
            if (HttpContext.Session.GetString("UserRole") != "Farmer")
            {
                return RedirectToAction("LoginView", "Account");
            }
            if (ModelState.IsValid)
            {
                int farmerId = HttpContext.Session.GetInt32("UserId").Value;
                model.Farmer.FarmerId = farmerId;
                model.dateAdded = DateTime.Today;
                _context.products.Add(model);
                _context.SaveChanges();
                return RedirectToAction("ViewProducts");
            }
            return View(model);
        }
    }
}