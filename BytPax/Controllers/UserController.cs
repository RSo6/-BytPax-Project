using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BytPax.Controllers.instructions;
using BytPax.Models;

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

    public IActionResult RegisterUser(string fullName, string email, string password, UserRole role)
    {
        _userService.RegisterUser(fullName, email, password, role);
        return CreatedAtAction(nameof(GetUser), new { email = email }, null);
    }
}