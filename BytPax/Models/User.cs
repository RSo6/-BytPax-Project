using System;

namespace BytPax.Models;

public enum UserRole
{
    Visitor = 0,
    Admin = 1
}

public class User  {
    public int Id { get; private set; }
    public UserRole Role { get; set; }
    public string FullName { get; private set; }
    public string ImagePath { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
}