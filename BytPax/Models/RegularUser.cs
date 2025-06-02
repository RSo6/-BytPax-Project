using BytPax.Models.core;

namespace BytPax.Models;

public class RegularUser : User
{
    public RegularUser(string fullName, string email, string imagePath, string passwordHash)
        : base(fullName, email, imagePath, UserRole.Visitor, passwordHash) { }

    public override void PerformRoleSpecificAction()
    {
        Console.WriteLine("User can view analytics, news, and competitions.");
    }
}