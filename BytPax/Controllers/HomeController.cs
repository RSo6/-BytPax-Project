using Microsoft.AspNetCore.Mvc;
using BytPax.Models;
using BytPax.Repositories;
using System.Linq;

namespace BytPax.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository<Athlete> _athleteRepo;
        private readonly Repository<Article> _articleRepo;
        private readonly Repository<RecordHistory> _recordRepo;
        private readonly Repository<HistoricalEvent> _eventRepo;

        public HomeController(Repository<Athlete> athleteRepo, 
                              Repository<Article> articleRepo,
                              Repository<RecordHistory> recordRepo,
                              Repository<HistoricalEvent> eventRepo)
        {
            _athleteRepo = athleteRepo;
            _articleRepo = articleRepo;
            _recordRepo = recordRepo;
            _eventRepo = eventRepo;
        }

        public IActionResult Index()
        {
            var athletes = _athleteRepo.GetAll();
            var articles = _articleRepo.GetAll();
            var records = _recordRepo.GetAll();
            var events = _eventRepo.GetAll();

            var model = new
            {
                AthletesCount = athletes.Count(),
                ArticlesCount = articles.Count(),
                RecordsCount = records.Count(),
                EventsCount = events.Count(),
                Athletes = athletes,
                Articles = articles,
                Records = records,
                Events = events
            };

            return View(model);
        }

        // Пошук спортсменів
        public IActionResult SearchAthletes(string searchTerm)
        {
            var athletes = _athleteRepo.GetAll()
                .Where(a => string.IsNullOrEmpty(searchTerm) || a.FullName.Contains(searchTerm))
                .ToList();
            return Json(athletes); 
        }

        // Пошук подій
        public IActionResult SearchEvents(string searchTerm)
        {
            var events = _eventRepo.GetAll()
                .Where(e => string.IsNullOrEmpty(searchTerm) || e.Title.Contains(searchTerm))
                .ToList();
            return Json(events); 
        }

        // Пошук рекордів
        public IActionResult SearchRecords(string searchTerm)
        {
            var records = _recordRepo.GetAll()
                .Where(r => string.IsNullOrEmpty(searchTerm) || r.AthleteName.Contains(searchTerm))
                .ToList();
            return Json(records);  
        }
        
        // Пошук статей
        public IActionResult SearchArticles(string searchTerm)
        {
            var articles = _articleRepo.GetAll()
                .Where(a => string.IsNullOrEmpty(searchTerm) || a.Topic.Contains(searchTerm))
                .ToList();
            return Json(articles);
        }
    }
}
