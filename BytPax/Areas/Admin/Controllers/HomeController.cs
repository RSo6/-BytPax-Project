using BytPax.Models;
using BytPax.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BytPax.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly Repository<Article> _articleRepo;
        private readonly Repository<Athlete> _athleteRepo;

        public HomeController(Repository<Article> articleRepo, Repository<Athlete> athleteRepo)
        {
            _articleRepo = articleRepo;
            _athleteRepo = athleteRepo;
        }

        public IActionResult Index()
        {
            var athletes = _athleteRepo.GetAll();
            return View(athletes);
        }

        public IActionResult AthleteDetails(int id)
        {
            var athlete = _athleteRepo.GetById(id);
            if (athlete == null)
            {
                return NotFound();
            }
            return View(athlete);
        }

        public IActionResult ArticleDetails(int id)
        {
            var article = _articleRepo.GetById(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }
    }
}