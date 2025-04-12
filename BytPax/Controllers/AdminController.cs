using Microsoft.AspNetCore.Mvc;
using BytPax.Models;
using BytPax.Instructions;

namespace BytPax.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository<Athlete> _athleteRepo;
        private readonly IRepository<Article> _articleRepo;

        // Локальний список категорій
        private readonly List<Category> _categories = new List<Category>
        {
            new Category(1, "Football", "Football players"),
            new Category(2, "Boxing", "Boxers"),
            new Category(3, "Swimming", "Swimmers")
        };

        public AdminController(IRepository<Athlete> athleteRepo, IRepository<Article> articleRepo)
        {
            _athleteRepo = athleteRepo;
            _articleRepo = articleRepo;
        }

        // --- Головна сторінка адмін-панелі ---
        public IActionResult Index()
        {
            var athletes = _athleteRepo.GetAll().ToList();
            foreach (var athlete in athletes)
            {
                athlete.Category = _categories.FirstOrDefault(c => c.Id == athlete.CategoryId);
            }

            var articles = _articleRepo.GetAll().ToList();
            foreach (var article in articles)
            {
                article.Category = _categories.FirstOrDefault(c => c.Id == article.CategoryId);
            }

            var model = new AdminManagementViewModel
            {
                Athletes = athletes,
                Articles = articles
            };

            return View(model);
        }

        // ---------- ATHLETES ----------
        public IActionResult CreateAthlete()
        {
            ViewBag.Categories = _categories;
            return View();
        }

        [HttpPost]
        public IActionResult CreateAthlete(Athlete model, int categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                model.Category = category;
                model.CategoryId = category.Id;
            }

            if (ModelState.IsValid)
            {
                _athleteRepo.Add(model);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categories;
            return View(model);
        }

        public IActionResult EditAthlete(int id)
        {
            var athlete = _athleteRepo.GetEntityById(id);
            if (athlete == null) return NotFound();

            athlete.Category = _categories.FirstOrDefault(c => c.Id == athlete.CategoryId);
            ViewBag.Categories = _categories;
            return View(athlete);
        }

        [HttpPost]
        public IActionResult EditAthlete(Athlete model, int categoryId)
        {
            var category = _categories.FirstOrDefault(c => c.Id == categoryId);
            if (category != null)
            {
                model.Category = category;
                model.CategoryId = category.Id;
            }

            if (ModelState.IsValid)
            {
                _athleteRepo.Update(model);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categories;
            return View(model);
        }

        public IActionResult DeleteAthlete(int id)
        {
            var athlete = _athleteRepo.GetEntityById(id);
            if (athlete == null) return NotFound();
            return View(athlete);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAthleteConfirmed(int id)
        {
            _athleteRepo.Delete(id);
            return RedirectToAction("Index");
        }

        // ---------- ARTICLES ----------
        public IActionResult CreateArticle()
        {
            ViewBag.Categories = _categories;
            return View();
        }

        [HttpPost]
        public IActionResult CreateArticle(Article model)
        {
            var category = _categories.FirstOrDefault(c => c.Id == model.CategoryId);
            if (category != null)
            {
                model.Category = category;
            }

            if (ModelState.IsValid)
            {
                _articleRepo.Add(model);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categories;
            return View(model);
        }

        public IActionResult EditArticle(int id)
        {
            var article = _articleRepo.GetEntityById(id);
            if (article == null) return NotFound();

            article.Category = _categories.FirstOrDefault(c => c.Id == article.CategoryId);
            ViewBag.Categories = _categories;
            return View(article);
        }

        [HttpPost]
        public IActionResult EditArticle(Article model)
        {
            var category = _categories.FirstOrDefault(c => c.Id == model.CategoryId);
            if (category != null)
            {
                model.Category = category;
            }

            if (ModelState.IsValid)
            {
                _articleRepo.Update(model);
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _categories;
            return View(model);
        }

        public IActionResult DeleteArticle(int id)
        {
            var article = _articleRepo.GetEntityById(id);
            if (article == null) return NotFound();
            return View(article);
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
