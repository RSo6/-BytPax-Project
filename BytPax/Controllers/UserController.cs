using Microsoft.AspNetCore.Mvc;
using BytPax.Models;
using BytPax.Models.core;
using System.Collections.Generic;

namespace BytPax.Controllers
{
    public class UserController : Controller
    {
        // GET: User/Cabinet
        public IActionResult Cabinet()
        {
            // Створюємо фіктивні дані для профілю користувача
            var userProfile = new UserProfileViewModel
            {
                Username = "ExampleUser",  // Назва користувача
                AvatarUrl = "/images/avatars/default.png",  
                SavedArticles = new List<Article>  
                {
                    new Article { Topic = "Article 1", BodyText = "Content of article 1" },
                    new Article { Topic = "Article 2", BodyText = "Content of article 2" }
                }
            };

            return View(userProfile);
        }
    }
}