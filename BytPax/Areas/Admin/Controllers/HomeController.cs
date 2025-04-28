using BytPax.Models;
using BytPax.Models.core;
using BytPax.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BytPax.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly Repository<Article> _articleRepo;
        private readonly Repository<Athlete> _athleteRepo;
        private readonly Repository<RecordHistory> _recordHistoryRepo;
        private readonly Repository<HistoricalEvent> _historicalEventRepo; // Додаємо репозиторій для історичних подій
        private readonly Repository<User> _userRepo;

        public HomeController(
            Repository<Article> articleRepo,
            Repository<Athlete> athleteRepo,
            Repository<RecordHistory> recordHistoryRepo,
            Repository<HistoricalEvent> historicalEventRepo, // Історичні події
            Repository<User> userRepo)
        {
            _articleRepo = articleRepo;
            _athleteRepo = athleteRepo;
            _recordHistoryRepo = recordHistoryRepo;
            _historicalEventRepo = historicalEventRepo; // Історичні події
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            // Отримуємо всі дані для моніторингу
            var athletesCount = _athleteRepo.GetAll().Count();
            var articlesCount = _articleRepo.GetAll().Count();
            var recordHistoriesCount = _recordHistoryRepo.GetAll().Count();
            var historicalEventsCount = _historicalEventRepo.GetAll().Count(); // Кількість історичних подій
            var usersCount = _userRepo.GetAll().Count();
            var adminCount = _userRepo.GetAll().Count(u => u.Role.ToString() == "Admin");

            // Передаємо ці дані в представлення
            ViewBag.AthletesCount = athletesCount;
            ViewBag.ArticlesCount = articlesCount;
            ViewBag.RecordHistoriesCount = recordHistoriesCount;
            ViewBag.HistoricalEventsCount = historicalEventsCount; // Кількість історичних подій
            ViewBag.UsersCount = usersCount;
            ViewBag.AdminCount = adminCount;

            return View();
        }
    }
}
