using BCrypt.Net;
using BytPax.Models;
using BytPax.Models.core;
using BytPax.Repositories;

namespace BytPax.Services;

public class AuthService
{
    private readonly Repository<User> _userRepository;

    public AuthService(Repository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Register(string fullName, string email, string imagePath, string password, User.UserRole role)
    {
        if (_userRepository.GetAll().Any(u => u.Email == email))
        {
            Console.WriteLine("User with this email already exists.");
            return false;
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        User newUser = role switch
        {
            User.UserRole.Admin => new AdminUser(fullName, email, imagePath, passwordHash),
            User.UserRole.Visitor => new RegularUser(fullName, email, imagePath, passwordHash),
            _ => throw new ArgumentOutOfRangeException()
        };

        _userRepository.Add(newUser);
        Console.WriteLine("User registered successfully.");
        return true;
    }

    public User? Login(string email, string password)
    {
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            Console.WriteLine("User not found.");
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            Console.WriteLine("Invalid password.");
            return null;
        }

        Console.WriteLine($"Welcome, {user.FullName}!");
        user.PerformRoleSpecificAction();
        return user;
    }
}