using Microsoft.AspNetCore.Mvc;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class employeeController : Controller
    {
        private readonly applicationDBContext _context;

        public employeeController(applicationDBContext context)
        {
            _context = context;
        }

        public IActionResult addFarmerView()
        {
            return View();
        }
    }
}
