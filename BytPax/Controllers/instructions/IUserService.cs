using BytPax.Models;

namespace BytPax.Controllers.instructions;

public interface IUserService  {
    User GetUserByEmail(string email);
    void RegisterUser(string fullName, string email, string password, UserRole role);
    bool Authenticate(string email, string password);
}