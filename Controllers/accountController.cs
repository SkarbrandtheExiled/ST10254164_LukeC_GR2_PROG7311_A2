using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices;

//------------------------------------START OF FILE------------------------------------//
namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class accountController : Controller
    {
        private readonly IFarmerServices _farmerService;
        
        private readonly IEmployeeRepository _employeeRepository;

        public accountController(
            IFarmerServices farmerService,
            
            IEmployeeRepository employeeRepository)
        {
            _farmerService = farmerService;
            
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult loginView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> loginViewAsync(string username, string password)
        {
            var role = ""; // determine role

            var employee = await _employeeRepository.GetEmployeeByCredentialsAsync(username, password);
            if (employee != null)
            {
                role = "employee";
            }
            else
            {
                // Check if it's a farmer
                var farmer = await _farmerService.GetFarmerByCredentials(username, password);
                if (farmer != null)
                {
                    role = "farmer";
                }
            }

            if (string.IsNullOrEmpty(role))
            {
                ViewBag.LoginError = "Invalid username or password.";
                return View();
            }

            //store user info in session 
            HttpContext.Session.SetString("User", username);
            HttpContext.Session.SetString("Role", role);

            //redirect based on role
            if (role == "farmer")
                return RedirectToAction("farmerDashboard", "farmer");
            else
                return RedirectToAction("employeeDashboard", "employee");
        }

        public IActionResult logout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            // Redirect to the home view
            return RedirectToAction("Index", "Home");
        }
    }
}
//------------------------------------END OF FILE------------------------------------//