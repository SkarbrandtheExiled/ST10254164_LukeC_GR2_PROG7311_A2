using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices;
using System.Security.Claims;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    //---------------------------------START OF FILE--------------------------------//
    //This controller is responsible for handling product-related actions for farmers
    [Authorize(Roles = "Farmer")]
    public class productController : Controller
    {
        private readonly IProductServices _productService;
        private readonly IFarmerServices _farmerService;
        private readonly ILogger<productController> _logger;

        public productController(
            IProductServices productService,
            IFarmerServices farmerService, // this code is used to get the farmer's information from the services and repositories 
            ILogger<productController> logger)
        {
            _productService = productService;
            _farmerService = farmerService;
            _logger = logger;
        }

        private async Task<farmerModel?> GetCurrentFarmerAsync()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                return null;
            return await _farmerService.GetFarmerByUserIdAsync(userId);
        }

        public async Task<IActionResult> FarmerProducts() // this method is used to get the farmer's products from the database and display them on the view
        {
            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null)
            {
                _logger.LogWarning("Farmer profile not found for logged-in user. Redirecting to login.");
                return RedirectToAction("Login", "Account");
            }

            var products = await _productService.GetProductsForFarmerAsync(farmer.Id);
            ViewBag.FarmerName = farmer.Name; //

            var viewModel = new farmerDashboardModel
            {
                NewProduct = new productModel { ProductionDate = DateTime.Today },// sets the production date of the new product to today's date (can be modified if desired)
                MyProducts = products
            };

            return View("~/Views/Farmer/farmerDashboard.cshtml", viewModel);
        }

        [HttpGet]
        public IActionResult addProductView()
        {
            return View("~/Views/Farmer/addProductView.cshtml"); // this code is supposed to get the addProductView for the farmer (did not work)
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //this method is used to add a new product for the farmer
        public async Task<IActionResult> addProductView(farmerDashboardModel viewModel)
        {
            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null)
                return RedirectToAction("Login", "Account");

            var productToAdd = viewModel.NewProduct;

            ModelState.Remove("NewProduct.Id");
            ModelState.Remove("NewProduct.FarmerId");
            ModelState.Remove("NewProduct.Farmer");
            ModelState.Remove("NewProduct.AddedDate"); // this code is used to remove the unwanted data from the model state    
            ModelState.Remove("MyProducts");

            if (ModelState.IsValid && productToAdd != null)
            {
                try
                {
                    await _productService.AddProductForFarmerAsync(productToAdd, farmer.Id);
                    TempData["SuccessMessage"] = "Product added successfully!";
                    return RedirectToAction(nameof(FarmerProducts)); //redirects the user to the farmer's product page after adding a new product
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Error adding product for farmer {FarmerId}", farmer.Id);
                    ModelState.AddModelError("", "An error occurred while adding the product. Please try again.");
                }
            }

            var existingProducts = await _productService.GetProductsForFarmerAsync(farmer.Id);
            ViewBag.FarmerName = farmer.Name; // used to get the farmer's name from the database and display it on the view
            var newViewModel = new farmerDashboardModel
            {
                NewProduct = productToAdd ?? new productModel { ProductionDate = DateTime.Today }, //adds the new product to the chosen view 
                MyProducts = existingProducts
            };
            return View("~/Views/Home/farmerDashboard.cshtml", newViewModel);
        }
    }
}
//---------------------------------END OF FILE--------------------------------//
