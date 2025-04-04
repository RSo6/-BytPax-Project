
using BytPax.Repositories;
using BytPax.Models;
using Microsoft.AspNetCore.Mvc;

namespace BytPax.Controllers
{
    public class AdminManagement : Controller
    {
        private readonly Repository<Article> _articleRepo;
        private readonly Repository<Athlete> _athleteRepo;

        public AdminManagement()
        {
            _articleRepo = new Repository<Article>();
            _athleteRepo = new Repository<Athlete>();
        }

        public ActionResult Index()
        {
            var model = new AdminManagementViewModel
            {
                Articles = _articleRepo.GetAll(),
                Athletes = _athleteRepo.GetAll()
            };
            return View(model);
        }

        // --- Додавання статті ---
        public ActionResult CreateArticle() => View();

        [HttpPost]
        public ActionResult CreateArticle(Article article)
        {
            if (!ModelState.IsValid) return View(article);

            _articleRepo.Add(article);
            return RedirectToAction("Index");
        }

        // --- Оновлення статті ---
        public ActionResult EditArticle(long id) => View(_articleRepo.GetEntityById(id));

        [HttpPost]
        public ActionResult EditArticle(Article article)
        {
            if (!ModelState.IsValid) return View(article);

            _articleRepo.Update(article);
            return RedirectToAction("Index");
        }

        // --- Видалення статті ---
        public ActionResult DeleteArticle(long id)
        {
            _articleRepo.Delete(id);
            return RedirectToAction("Index");
        }

        // --- Додавання атлета ---
        public ActionResult CreateAthlete() => View();

        [HttpPost]
        public ActionResult CreateAthlete(Athlete athlete)
        {
            if (!ModelState.IsValid) return View(athlete);

            _athleteRepo.Add(athlete);
            return RedirectToAction("Index");
        }

        // --- Оновлення атлета ---
        public ActionResult EditAthlete(long id) => View(_athleteRepo.GetEntityById(id));

        [HttpPost]
        public ActionResult EditAthlete(Athlete athlete)
        {
            if (!ModelState.IsValid) return View(athlete);

            _athleteRepo.Update(athlete);
            return RedirectToAction("Index");
        }

        // --- Видалення атлета ---
        public ActionResult DeleteAthlete(long id)
        {
            _athleteRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
