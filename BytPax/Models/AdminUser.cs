using BytPax.Models.core;

namespace BytPax.Models;

public class AdminUser : User {
    public AdminUser(string fullName, string email, string imagePath, string passwordHash)
        : base(fullName, email, imagePath, UserRole.Admin,  passwordHash) { }
    
    public override void PerformRoleSpecificAction()
    {
        Console.WriteLine("Admin can manage analytics, news, and historical data.");
    }
}


