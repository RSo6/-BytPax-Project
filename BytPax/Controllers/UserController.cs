using Microsoft.AspNetCore.Mvc;
using BytPax.Areas.Admin.Models;
using BytPax.Models;
using System.Collections.Generic;
using System.Linq;

namespace BytPax.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Cabinet()
        {

            var savedArticles = new List<Article>
            {
                new Article { Topic = "Article 1", BodyText = "Content of article 1", CategoryId = 1 },
                new Article { Topic = "Article 2", BodyText = "Content of article 2", CategoryId = 2 }
            };


            var savedArticleViewModels = savedArticles.Select(article => new ArticleCreateViewModel
            {
                Topic = article.Topic,
                BodyText = article.BodyText,
                CategoryId = article.CategoryId,
                ImagePath = article.ImagePath
            }).ToList();


            var userProfile = new UserProfileViewModel
            {
                Username = "ExampleUser",
                AvatarUrl = "/images/avatars/default.png",
                SavedArticles = savedArticleViewModels,

                FavoriteAthletes = new List<AthleteCreateViewModel>
                {
                    new AthleteCreateViewModel
                    {
                        Id = 1,
                        FullName = "John Doe",
                        Age = 28,
                        Country = "USA",
                        City = "New York",
                        Description = "Top sprinter"
                    },
                    new AthleteCreateViewModel
                    {
                        Id = 2,
                        FullName = "Jane Smith",
                        Age = 24,
                        Country = "UK",
                        City = "London",
                        Description = "Marathon runner"
                    }
                }
            };

            return View(userProfile);
        }
    }
}
