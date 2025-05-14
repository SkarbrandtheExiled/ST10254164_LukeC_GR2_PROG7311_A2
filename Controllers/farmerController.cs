using Microsoft.AspNetCore.Mvc;
using ST10254164_LukeC_GR2_PROG7311_A2.Services;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class farmerController : Controller
    {
        private readonly IProductServices _productService;

        public farmerController(IProductServices productService)
        {
            _productService = productService;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addProductView(productModel model)
        {
            if(model == null)
            {
                return View(new productModel());
            }
            
            if (ModelState.IsValid)
            {
                model.farmerName = HttpContext.Session.GetString("User");
                model.dateAdded = DateTime.Now;
                await _productService.AddProductAsync(model);
                return RedirectToAction("viewProducts");
            }
            return View(model);
        }

        public async Task<IActionResult> viewProducts()
        {
            if (HttpContext.Session.GetString("Role") != "farmer")
                return RedirectToAction("loginView", "account");

            var currentFarmer = HttpContext.Session.GetString("User");
            var products = await _productService.GetProductsByFarmerAsync(currentFarmer);
            ViewBag.Categories = await _productService.GetCategoriesAsync(currentFarmer);

            return View(products);
        }
    }
}