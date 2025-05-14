using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using System.Text;

//------------------------------------START OF FILE------------------------------------//
namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class accountController : Controller
    {
        private readonly applicationDBContext _context;

        public accountController(applicationDBContext context) // Inject DbContext
        {
            _context = context;
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return enteredPassword == storedPassword;
        }

        [HttpGet]
        public IActionResult loginView()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> loginViewAsync(string username, string password) 
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.LoginError = "Username and password are required.";
                return View();
            }

            var role = "";
            string actualUserName = null; 


            var employee = await _context.employees.FirstOrDefaultAsync(e => e.employeeName == username);

            if (employee != null && VerifyPassword(password, employee.Password))
            {
                role = "employee";
                actualUserName = employee.employeeName;
                HttpContext.Session.SetString("User", actualUserName);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("EmployeeName", actualUserName); // Specific employee name
                return RedirectToAction("employeeDashboard", "employee");
            }
            else
            {
                // If not an employee, try to find a farmer by username (assuming farmerName is the login username)
                var farmer = await _context.farmers.FirstOrDefaultAsync(f => f.farmerName == username); // Or f.Email == username

                if (farmer != null && VerifyPassword(password, farmer.Password))
                {
                    role = "farmer";
                    actualUserName = farmer.farmerName;
                    HttpContext.Session.SetString("User", actualUserName);
                    HttpContext.Session.SetString("Role", role);
                    HttpContext.Session.SetString("FarmerName", actualUserName);
                    HttpContext.Session.SetString("FarmerID", farmer.farmerID.ToString());
                    return RedirectToAction("farmerDashboard", "farmer");
                }
                else
                {
                    // If no match is found
                    ViewBag.LoginError = "Invalid username or password.";
                    return View();
                }
            }
        }

        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("loginView", "account");
        }
    }
}
//------------------------------------END OF FILE------------------------------------//