using BytPax.Models.core;

namespace BytPax.Models;

public class RegisterViewModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string ImagePath { get; set; } 
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public User.UserRole Role { get; set; }
}