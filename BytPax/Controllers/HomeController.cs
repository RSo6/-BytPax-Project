// Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;
using BytPax.Services;

namespace BytPax.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchService _searchService;

        public HomeController(SearchService homeService)
        {
            _searchService = homeService;
        }

        public IActionResult Index()
        {
            var model = _searchService.GetDashboardData();
            return View(model);
        }

        public IActionResult SearchAthletes(string searchTerm)
        {
            var athletes = _searchService.SearchAthletes(searchTerm);
            return Json(athletes);
        }

        public IActionResult SearchEvents(string searchTerm)
        {
            var events = _searchService.SearchEvents(searchTerm);
            return Json(events);
        }

        public IActionResult SearchRecords(string searchTerm)
        {
            var records = _searchService.SearchRecords(searchTerm);
            return Json(records);
        }

        public IActionResult SearchArticles(string searchTerm)
        {
            var articles = _searchService.SearchArticles(searchTerm);
            return Json(articles);
        }
    }
}