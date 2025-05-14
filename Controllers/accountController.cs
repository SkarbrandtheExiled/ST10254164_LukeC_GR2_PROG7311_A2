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

        public accountController(applicationDBContext context)
        {
            _context = context;
        }

        public IActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginView(string username, string password)
        {
            // Attempt to authenticate as a farmer first
            var farmer = _context.farmers.FirstOrDefault(f => f.FarmerName == username && f.Password == password);
            if (farmer != null)
            {
                HttpContext.Session.SetString("UserRole", "Farmer");
                HttpContext.Session.SetInt32("UserId", farmer.FarmerId); // Store FarmerId as int
                return RedirectToAction("farmerDashboard", "farmer");
            }

            // If not a farmer, attempt to authenticate as an employee
            var employee = _context.employees.FirstOrDefault(e => e.EmployeeName == username && e.Password == password); //this must be set to the username and password of the employee
            if (employee != null)
            {
                HttpContext.Session.SetString("UserRole", "Employee");
                HttpContext.Session.SetInt32("UserId", employee.EmployeeId); // Store EmployeeId as int
                return RedirectToAction("employeeDashboard", "employee");
            }

            // If neither farmer nor employee, show error
            ViewBag.LoginError = "Invalid username or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear all session data
            return RedirectToAction("loginView", "Account"); // Redirect to the login page
        }
    }
}
//------------------------------------END OF FILE------------------------------------//