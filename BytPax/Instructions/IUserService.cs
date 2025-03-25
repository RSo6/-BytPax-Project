using BytPax.Models.core;

namespace BytPax.Instructions;

public interface IUserService  {
    User GetUserByEmail(string email);
    void RegisterUser(string fullName, string email, string password, User.UserRole role);
    bool Authenticate(string email, string password);
}