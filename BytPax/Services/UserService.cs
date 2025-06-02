using BytPax.Instructions;
using BytPax.Models;
using BytPax.Models.core;

namespace BytPax.Services
{
    public class UserService : IUserService
    {
        private readonly IDataStorage<User> _userStorage;

        public UserService(IDataStorage<User> userStorage)
        {
            _userStorage = userStorage;
        }

        public User GetUserByEmail(string email)
        {
            return _userStorage.GetAll().FirstOrDefault(u => u.Email == email);
        }

        public void RegisterUser(string fullName, string email, string password, User.UserRole role)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new RegularUser(fullName, email, "/images/default-avatar.png", passwordHash);
            _userStorage.Add(user);
            _userStorage.Save();
        }

        public bool Authenticate(string email, string password)
        {
            var user = GetUserByEmail(email);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }

        public void UpdateUsername(int userId, string newUsername)
        {
            var user = _userStorage.GetById(userId);
            if (user != null && !string.IsNullOrWhiteSpace(newUsername))
            {
                typeof(User).GetProperty("FullName")?.SetValue(user, newUsername);
                _userStorage.Update(user);
                _userStorage.Save();
            }
        }
    }
}
