using Microsoft.AspNetCore.Mvc;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

//------------------------------------START OF FILE------------------------------------//
namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class accountController : Controller
    {
        private readonly List<(string username, string password, string role)> users = new()
    {
        ("farmer", "password123", "farmer"),
        ("employee", "employee", "employee")
    };
        [HttpGet]
        public IActionResult loginView()
        {
            return View();
        }
        [HttpPost]
        public IActionResult loginView(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.username == username && u.password == password);

            if (user == default)
            {
                ViewBag.LoginError = "Invalid username or password.";
                return View();
            }

            //store user info in session 
            HttpContext.Session.SetString("User", username);
            HttpContext.Session.SetString("Role", user.role);

            //redirect based on role
            if (user.role == "farmer")
                return RedirectToAction("farmerDashboard", "farmer");
            else
                return RedirectToAction("employeeDashboard", "employee");
        }
        public IActionResult logout()
        {
            // Clear the session
            HttpContext.Session.Clear();
            // Redirect to the home view
            return RedirectToAction("Index");
        }
    }
}
//------------------------------------END OF FILE------------------------------------//