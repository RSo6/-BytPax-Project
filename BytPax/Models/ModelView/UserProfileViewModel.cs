using System.Collections.Generic;
using BytPax.Areas.Admin.Models;

namespace BytPax.Models
{
    public class UserProfileViewModel
    {
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public List<ArticleCreateViewModel> SavedArticles { get; set; }
        public List<AthleteCreateViewModel> FavoriteAthletes { get; set; }

    }
}
