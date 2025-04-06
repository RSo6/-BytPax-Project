using Microsoft.AspNetCore.Mvc;
using BytPax.Models;
using BytPax.Instructions;

namespace BytPax.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository<Athlete> _athleteRepo;
        private readonly IRepository<Article> _articleRepo;

        public AdminController(IRepository<Athlete> athleteRepo, IRepository<Article> articleRepo)
        {
            _athleteRepo = athleteRepo;
            _articleRepo = articleRepo;
        }

        // --- Головна сторінка адмін-панелі ---
        public IActionResult Index()
        {
            var model = new AdminManagementViewModel
            {
                Athletes = _athleteRepo.GetAll().ToList(),
                Articles = _articleRepo.GetAll().ToList()
            };
            return View(model); // буде шукати Views/Admin/Index.cshtml
        }

        // ---------- ATHLETES ----------
        public IActionResult CreateAthlete() => View();

        [HttpPost]
        public IActionResult CreateAthlete(Athlete model)
        {
            if (ModelState.IsValid)
            {
                _athleteRepo.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult EditAthlete(int id)
        {
            var athlete = _athleteRepo.GetEntityById(id);
            if (athlete == null) return NotFound();
            return View(athlete);
        }

        [HttpPost]
        public IActionResult EditAthlete(Athlete model)
        {
            if (ModelState.IsValid)
            {
                _athleteRepo.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult DeleteAthlete(int id)
        {
            var athlete = _athleteRepo.GetEntityById(id);
            if (athlete == null) return NotFound();
            return View(athlete); // Показує DeleteAthlete.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAthleteConfirmed(int id)
        {
            _athleteRepo.Delete(id);
            return RedirectToAction("Index");
        }

        // ---------- ARTICLES ----------
        public IActionResult CreateArticle() => View();

        [HttpPost]
        public IActionResult CreateArticle(Article model)
        {
            if (ModelState.IsValid)
            {
                _articleRepo.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult EditArticle(int id)
        {
            var article = _articleRepo.GetEntityById(id);
            if (article == null) return NotFound();
            return View(article);
        }

        [HttpPost]
        public IActionResult EditArticle(Article model)
        {
            if (ModelState.IsValid)
            {
                _articleRepo.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult DeleteArticle(int id)
        {
            var article = _articleRepo.GetEntityById(id);
            if (article == null) return NotFound();
            return View(article); // Показує DeleteArticle.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteArticleConfirmed(int id)
        {
            _articleRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
