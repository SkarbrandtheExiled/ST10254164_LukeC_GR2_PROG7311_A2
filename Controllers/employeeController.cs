using Microsoft.AspNetCore.Mvc;
using ST10254164_LukeC_GR2_PROG7311_A2.Services;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class employeeController : Controller
    {
        private readonly IFarmerServices _farmerService;
        private readonly IProductServices _productService;

        public employeeController(IFarmerServices FarmerService, IProductServices ProductService)
        {
            _farmerService = FarmerService;
            _productService = ProductService;
        }

        public IActionResult employeeDashboard()
        {
            if (HttpContext.Session.GetString("Role") != "employee")
                return RedirectToAction("loginView", "account");

            return View();
        }

        public IActionResult addFarmerView()
        {
            if (HttpContext.Session.GetString("Role") != "employee")
                return RedirectToAction("loginView", "account");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addFarmerView(string farmerName, string email, string password)
        {
            if (HttpContext.Session.GetString("Role") != "employee")
                return RedirectToAction("loginView", "account");

            if (await _farmerService.FarmerExistsAsync(farmerName))
            {
                ViewBag.Message = "Farmer with that name already exists.";
                return View();
            }

            await _farmerService.CreateFarmerAsync(farmerName, email, password);
            return RedirectToAction("employeeDashboard");
        }

        [HttpGet]
        public async Task<IActionResult> viewAllProducts(string farmerFilter = "", string categoryFilter = "", DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            if (HttpContext.Session.GetString("Role") != "employee")
                return RedirectToAction("loginView", "account");

            var products = await _productService.GetFilteredProductsAsync(farmerFilter, categoryFilter, dateFrom, dateTo);
            ViewBag.Farmers = await _productService.GetFarmerNamesAsync();
            ViewBag.Categories = await _productService.GetCategoriesAsync();

            ViewBag.CurrentFarmerFilter = farmerFilter;
            ViewBag.CurrentCategoryFilter = categoryFilter;
            ViewBag.CurrentDateFrom = dateFrom;
            ViewBag.CurrentDateTo = dateTo;

            return View(products);
        }
    }
}