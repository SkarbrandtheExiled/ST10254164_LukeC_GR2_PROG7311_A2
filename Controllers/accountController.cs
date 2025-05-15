using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Repositories.employeeRepository;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.accountServices;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices;
using System.Security.Claims;

//------------------------------------START OF FILE------------------------------------//
namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    public class accountController : Controller
    {
            private readonly IAccountService _accountService;
            private readonly ILogger<accountController> _logger;

            public accountController(IAccountService accountService, ILogger<accountController> logger)
            {
                _accountService = accountService;
                _logger = logger;
            }


            [HttpGet]
            public IActionResult loginView(string? returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> loginView(loginModel model, string? returnUrl = null)
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (ModelState.IsValid)
                {
                    var user = await _accountService.ValidateCredentialsAsync(model.Username, model.Password);

                    if (user != null)
                    {
                        _logger.LogInformation("User {Username} logged in successfully.", user.Username);

                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Store User ID
                        new Claim(ClaimTypes.Role, user.Role)

                    };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {

                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            // Redirect based on role
                            if (user.Role == "Farmer")
                            {

                                return RedirectToAction("FarmerProducts", "Product");
                            }
                            else if (user.Role == "Employee")
                            {

                                return RedirectToAction("Index", "Home");
                            }
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Invalid login attempt for username {Username}.", model.Username);
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }
                }
                return View(model);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _logger.LogInformation("User logged out.");
                return RedirectToAction("Index", "Home");
            }
        }
    }
//------------------------------------END OF FILE------------------------------------//