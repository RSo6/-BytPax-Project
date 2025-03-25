using Microsoft.AspNetCore.Mvc;
using BytPax.Instructions;
using BytPax.Models.core;

namespace Controllers;

public class UserController : Controller  {
    
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult GetUser(string email)
    {
        var user = _userService.GetUserByEmail(email);
        if (user == null)
            return NotFound("User not found");

        return Ok(user);
    }

    public IActionResult RegisterUser(string fullName, string email, string password, User.UserRole role)
    {
        _userService.RegisterUser(fullName, email, password, role);
        return CreatedAtAction(nameof(GetUser), new { email = email }, null);
    }
}