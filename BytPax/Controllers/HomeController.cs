using Microsoft.AspNetCore.Mvc;
using BytPax.Services;
using BytPax.Models;
using BytPax.Repositories;
using System.Linq;

namespace BytPax.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchService _searchService;
        private readonly Repository<Article> _articleRepository;
        private readonly Repository<Category> _categoryRepository;

        public HomeController(
            SearchService searchService,
            Repository<Article> articleRepo,
            Repository<Category> categoryRepo)
        {
            _searchService = searchService;
            _articleRepository = articleRepo;
            _categoryRepository = categoryRepo;
        }

        public IActionResult Index()
        {
            var articles = _articleRepository.GetAll()
                .Select(a => new
                {
                    a.Id,
                    a.Topic,
                    a.BodyText,
                    a.ImagePath,
                    CategoryName = _categoryRepository.GetById(a.CategoryId)?.Name
                }).ToList();

            return View(articles);
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

        [HttpGet]
        public IActionResult GetArticles()
        {
            var articles = _articleRepository.GetAll()
                .Select(a => new
                {
                    a.Id,
                    a.Topic,
                    a.BodyText,
                    a.ImagePath,
                    a.CategoryId,
                    CategoryName = _categoryRepository.GetById(a.CategoryId)?.Name
                });

            return Json(articles);
        }
    }
}
