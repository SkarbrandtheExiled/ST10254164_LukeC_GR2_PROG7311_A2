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

        public IActionResult viewProducts()
        {
            var products = _context.products.ToList(); //←error here when clicking the view products button, unable to open the database
            return View(products);
        }
    }
}
