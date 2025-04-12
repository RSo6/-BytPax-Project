using BytPax.Instructions;
using BytPax.Models;
using Microsoft.AspNetCore.Mvc;

namespace BytPax.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Athlete> _athleteRepository;

        public HomeController(IRepository<Article> articleRepository, IRepository<Athlete> athleteRepository)
        {
            _articleRepository = articleRepository;
            _athleteRepository = athleteRepository;
        }

        // GET: Admin/Home
        public IActionResult Index()
        {
            var articles = _articleRepository.GetAll();
            var athletes = _athleteRepository.GetAll();
            
            var model = new AdminViewModel
            {
                Articles = articles,
                Athletes = athletes
            };

            return View(model);
        }
    }
}