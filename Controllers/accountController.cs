using Microsoft.AspNetCore.Mvc;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class accountController : Controller
    {
        private readonly applicationDBContext _context;
        public accountController(applicationDBContext _context)
        {
            _context = context;
        }
        public IActionResult loginView()
        {
            return View();
        }
    }
}
