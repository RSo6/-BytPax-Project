using BytPax.Models;
using BytPax.Repositories;
using BytPax.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using BytPax.Instructions;
using BytPax.Models.core;

namespace BytPax.Tests.Services;

[TestFixture]
public class AuthServiceTests
{
    private Mock<IDataStorage<User>> _mockRepository = null!;
    private AuthService _authService = null!;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new Mock<IDataStorage<User>>();
        _authService = new AuthService(_mockRepository.Object);
    }

    [Test]
    public void Register_WhenEmailExists_ReturnsFalse()
    {
        // Arrange
        var existingUsers = new List<User> { new RegularUser("Name", "test@example.com", "", "hash") };
        _mockRepository.Setup(r => r.GetAll()).Returns(existingUsers);

        // Act
        var result = _authService.Register("New Name", "test@example.com", "", "1234", User.UserRole.Visitor);

        // Assert
        Assert.IsFalse(result);
        _mockRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Never);
    }

    [Test]
    public void Register_WhenNewEmail_AddsUserAndReturnsTrue()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetAll()).Returns(new List<User>());

        // Act
        var result = _authService.Register("John Doe", "john@example.com", "", "password123", User.UserRole.Visitor);

        // Assert
        Assert.IsTrue(result);
        _mockRepository.Verify(r => r.Add(It.Is<User>(u => u.Email == "john@example.com")), Times.Once);
    }

    [Test]
    public void Login_WithCorrectCredentials_ReturnsUser()
    {
        // Arrange
        var password = "12345678";
        var hash = BCrypt.Net.BCrypt.HashPassword(password);
        var user = new RegularUser("Tester", "user@example.com", "", hash);

        _mockRepository.Setup(r => r.GetAll()).Returns(new List<User> { user });

        // Act
        var result = _authService.Login("user@example.com", password);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Tester", result?.FullName);
    }

    [Test]
    public void Login_WithWrongPassword_ReturnsNull()
    {
        // Arrange
        var hash = BCrypt.Net.BCrypt.HashPassword("correct");
        var user = new RegularUser("User", "user@example.com", "", hash);
        _mockRepository.Setup(r => r.GetAll()).Returns(new List<User> { user });

        // Act
        var result = _authService.Login("user@example.com", "wrong");

        // Assert
        Assert.IsNull(result);
    }
}
