using System.Text.Json.Serialization;

namespace BytPax.Models.core;

public abstract class User : BaseEntity {
    public enum UserRole
    {
        Visitor = 0,
        Admin = 1
    }
    public string FullName { get; protected set; }
    public string Email { get; protected set; }
    public string ImagePath { get; protected set; }
    public UserRole Role { get; protected set; }
    public string PasswordHash { get; protected set; }
   
    protected User(string fullName, string email, string imagePath, UserRole role, string passwordHash)
    {
        FullName = fullName;
        Email = email;
        ImagePath = imagePath;
        Role = role;
        PasswordHash = passwordHash;
    }

    public abstract void PerformRoleSpecificAction();
}