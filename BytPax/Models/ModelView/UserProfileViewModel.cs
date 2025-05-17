namespace BytPax.Models;

public class UserProfileViewModel
{
    public string Username { get; set; }
    public string AvatarUrl { get; set; }
    public List<Article> SavedArticles { get; set; } 
}