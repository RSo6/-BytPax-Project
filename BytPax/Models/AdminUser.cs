using BytPax.Models.core;

namespace BytPax.Models;

public class AdminUser : User {
    public AdminUser(string fullName, string email, string imagePath)
        : base(fullName, email, imagePath, UserRole.Admin) { }

    public override void PerformRoleSpecificAction()
    {
        Console.WriteLine("Admin can manage analytics, news, and historical data.");
    }
}


