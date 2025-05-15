using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Services;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    [Authorize(Roles = "Employee")] // Only Employees can access this
    public class employeeController : Controller
    {
        private readonly IFarmerServices _farmerService;
        private readonly IProductServices _productService;
        private readonly ILogger<employeeController> _logger;

        public employeeController(
            IFarmerServices farmerService,
            IProductServices productService,
            ILogger<employeeController> logger)
        {
            _farmerService = farmerService;
            _productService = productService;
            _logger = logger;
        }


        public async Task<IActionResult> employeeDashboard(employeeDashboardModel filterModel)
        {
            // Gets data needed for filtering options
            var farmers = await _farmerService.GetAllFarmersAsync();
            var farmerListItems = farmers
                .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name })
                .ToList();

            farmerListItems.Insert(0, new SelectListItem { Value = "", Text = "--- All Farmers ---" });

            // Get products based on filters

            var productsQuery = await _productService.GetAllProductsAsync();

            // Apply filters based on the submitted filterModel
            if (filterModel.SelectedFarmerId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.FarmerId == filterModel.SelectedFarmerId.Value);
            }
            if (!string.IsNullOrWhiteSpace(filterModel.FilterProductType))
            {
                productsQuery = productsQuery.Where(p => p.Category.Equals(filterModel.FilterProductType, StringComparison.OrdinalIgnoreCase));
            }
            if (filterModel.FilterStartDate.HasValue)
            {
                //  comparison ignores time part if only date is relevant
                productsQuery = productsQuery.Where(p => p.ProductionDate.Date >= filterModel.FilterStartDate.Value.Date);
            }
            if (filterModel.FilterEndDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate.Date <= filterModel.FilterEndDate.Value.Date);
            }

            //Prepare the ViewModel for the View
            var viewModel = new employeeDashboardModel
            {
                // Populate filter options for redisplay
                AvailableFarmers = farmerListItems,
                SelectedFarmerId = filterModel.SelectedFarmerId,
                FilterProductType = filterModel.FilterProductType,
                FilterStartDate = filterModel.FilterStartDate,
                FilterEndDate = filterModel.FilterEndDate,

                // Assign the filtered products
                Products = productsQuery.ToList() // Execute the query
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddFarmer()
        {
//new addFarmerModel()
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addFarmerModel(addFarmerModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model); // Return view with validation errors
            }


            var (success, errorMessage) = await _farmerService.CreateFarmerWithUserAsync(model);

            if (success)
            {
                _logger.LogInformation("New farmer {FarmerName} and user {Username} created successfully.", model.FarmerName, model.Username);
                TempData["SuccessMessage"] = $"Farmer '{model.FarmerName}' created successfully.";
                return RedirectToAction(nameof(employeeDashboard));
            }
            else
            {
                ModelState.AddModelError(string.Empty, errorMessage ?? "Failed to create farmer account.");
                return View("addFarmerModel", model);


            }
        }
    }
}