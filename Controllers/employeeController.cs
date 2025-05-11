using Microsoft.AspNetCore.Mvc;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class employeeController : Controller
    {
        public IActionResult addFarmerView()
        {
            return View();
        }
        public IActionResult filteredView()
        {
            return View();
        }
    }
}
