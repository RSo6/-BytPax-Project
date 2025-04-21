using System.Security.Claims;
using Microsoft.Extensions.Logging;
using BytPax.Data;
using BytPax.Models;
using BytPax.Models.core;
using BytPax.Services;
using BytPax.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BytPax.Controllers;

public class AuthController : Controller
{
    private readonly AuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(Repository<User> userRepository, ILogger<AuthController> logger)
    {
        _authService = new AuthService(userRepository);
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        if (model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError("", "Passwords do not match.");
            return View(model);
        }

        var success = _authService.Register(model.FullName, model.Email, model.ImagePath, model.Password, model.Role);

        if (success)
        {
            TempData["SuccessMessage"] = "You have successfully registered. Please log in."; // Повідомлення про успішну реєстрацію
            return RedirectToAction("Login");
        }
        else
        {
            TempData["ErrorMessage"] = "Email already exists."; // Повідомлення про помилку
            return View(model);
        }
    }

    [HttpGet]
    public ActionResult Login()
    {
        return View("~/Views/Auth/Login.cshtml");
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = _authService.Login(model.Email, model.Password);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            TempData["SuccessMessage"] = $"Welcome back, {user.FullName}!"; // Привітання після входу
            if (user.Role == Models.core.User.UserRole.Admin)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        TempData["ErrorMessage"] = "Invalid login credentials."; // Повідомлення про помилку
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        TempData["SuccessMessage"] = "You have successfully logged out."; // Повідомлення про успішний вихід
        return RedirectToAction("Index", "Home");
    }
}
