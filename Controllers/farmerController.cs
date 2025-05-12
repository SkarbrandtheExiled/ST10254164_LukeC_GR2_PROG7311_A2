using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class farmerController : Controller
    {
        private readonly applicationDBContext _context;
        public farmerController(applicationDBContext _context)
        {
            _context = context;
        }
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
