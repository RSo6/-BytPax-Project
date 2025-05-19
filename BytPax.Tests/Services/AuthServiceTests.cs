using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using BytPax.Models.core;
using BytPax.Repositories;
using BytPax.Services;
using BytPax.Models;

namespace BytPax.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<Repository<User>> _userRepositoryMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            // Repository<User> – абстрактний, мокати як virtual methods (або зробити інтерфейс)
            _userRepositoryMock = new Mock<Repository<User>>();
            _authService = new AuthService(_userRepositoryMock.Object);
        }

        [Fact]
        public void Register_UserAlreadyExists_ReturnsFalse()
        {
            // Arrange
            var existingUsers = new List<User> {
                new RegularUser("Test User", "test@example.com", "", "hashedpass")
            };

            _userRepositoryMock.Setup(r => r.GetAll()).Returns(existingUsers);

            // Act
            var result = _authService.Register("New User", "test@example.com", "", "password", User.UserRole.Visitor);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Register_NewUser_ReturnsTrue()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetAll()).Returns(new List<User>());
            _userRepositoryMock.Setup(r => r.Add(It.IsAny<User>()));

            // Act
            var result = _authService.Register("New User", "new@example.com", "", "password", User.UserRole.Visitor);

            // Assert
            Assert.True(result);
            _userRepositoryMock.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var password = "12345";
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new RegularUser("John Doe", "john@example.com", "", passwordHash);

            _userRepositoryMock.Setup(r => r.GetAll()).Returns(new List<User> { user });

            // Act
            var result = _authService.Login("john@example.com", password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("john@example.com", result.Email);
        }

        [Fact]
        public void Login_InvalidPassword_ReturnsNull()
        {
            // Arrange
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("correctPassword");
            var user = new RegularUser("John Doe", "john@example.com", "", passwordHash);

            _userRepositoryMock.Setup(r => r.GetAll()).Returns(new List<User> { user });

            // Act
            var result = _authService.Login("john@example.com", "wrongPassword");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Login_UserNotFound_ReturnsNull()
        {
            // Arrange
            _userRepositoryMock.Setup(r => r.GetAll()).Returns(new List<User>());

            // Act
            var result = _authService.Login("nonexistent@example.com", "any");

            // Assert
            Assert.Null(result);
        }
    }
}
