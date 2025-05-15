using Microsoft.AspNetCore.Mvc;
using ST10254164_LukeC_GR2_PROG7311_A2.Services;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{ // This was the intended controller for the farmer dashboard
    public class farmerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}