using Microsoft.AspNetCore.Mvc;
using BytPax.Areas.Admin.Models;
using BytPax.Models;
using System.Collections.Generic;
using System.Linq;

namespace BytPax.Controllers
{
    public class UserController : Controller
    {
        // “имчасово хардкодимо користувача
        private static RegularUser _currentUser = new RegularUser(
            "“естове ≤мТ€",
            "user@example.com",
            "/images/default-avatar.png",
            "dummyHash"
        );

        public IActionResult Cabinet()
        {
            var viewModel = new UserProfileViewModel
            {
                Username = _currentUser.FullName,
                AvatarUrl = _currentUser.ImagePath,
                SavedArticles = new List<ArticleCreateViewModel>
                {
                    new ArticleCreateViewModel { Topic = "як б≥гати швидше" },
                    new ArticleCreateViewModel { Topic = "’арчуванн€ дл€ спортсмен≥в" }
                },
                FavoriteAthletes = new List<AthleteCreateViewModel>()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult UpdateUsername(string newUsername)
        {
            if (!string.IsNullOrWhiteSpace(newUsername))
            {
                _currentUser.SetFullName(newUsername);
            }

            var viewModel = new UserProfileViewModel
            {
                Username = _currentUser.FullName,
                AvatarUrl = _currentUser.ImagePath,
                SavedArticles = new List<ArticleCreateViewModel>
                {
                    new ArticleCreateViewModel { Topic = "як б≥гати швидше" },
                    new ArticleCreateViewModel { Topic = "’арчуванн€ дл€ спортсмен≥в" }
                },
                FavoriteAthletes = new List<AthleteCreateViewModel>()
            };

            return View("Cabinet", viewModel);
        }
    }
}


