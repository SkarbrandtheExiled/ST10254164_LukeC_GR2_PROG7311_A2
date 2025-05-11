using Microsoft.AspNetCore.Mvc;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class farmerController : Controller
    {
        public IActionResult addProductView()
        {
            return View();
        }
        public IActionResult viewProducts()
        {
            return View();
        }
    }
}
