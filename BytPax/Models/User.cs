using System;
using System.Security.Cryptography;
using System.Text;
using BytPax.Models.core;

namespace BytPax.Models;

public enum UserRole
{
    Visitor = 0,
    Admin = 1
}

public class User : BaseEntity  {
    
    public UserRole Role { get; }
    public string FullName { get; }
    public string ImagePath { get; }
    public string Email { get; }
    public string Password { get; protected set; }
    
    public User(string fullName, string image, string email, UserRole role) 
        : base()
    {
        FullName = fullName;
        ImagePath = image;
        Email = email;
        Role = role;
    }

    public void SetPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            Password = Convert.ToBase64String(hash);
        }
    }
    
    
}