using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BytPax.Models;
using BytPax.Repositories;

namespace BytPax.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    // private readonly AthleteRepository _athleteRepo;
    // private readonly ArticleRepository _articleRepo;

    // Виключаємо параметри для репозиторіїв із конструктора
    public HomeController(ILogger<HomeController>? logger)
    {
        _logger = logger;
        
        // Створюємо репозиторії вручну
        // _athleteRepo = new AthleteRepository();  // Викликаємо конструктор без параметрів
        // _articleRepo = new ArticleRepository();  // Викликаємо конструктор без параметрів
    }

    public IActionResult Index()
    {
        // if (_athleteRepo.GetAll().Count() == 0)
        // {
        //     _athleteRepo.Add(new Athlete(25, "John Doe", "USA", 1, new Category(1, "Football", "TODO"), "New York", "A great athlete"));
        //     _athleteRepo.Add(new Athlete(30, "Jane Smith", "Canada", 1, new Category(1, "Football", "TODO"), "Toronto", "World record holder"));
        //     _athleteRepo.Add(new Athlete(22, "Samuel Green", "Australia", 2, new Category(2, "Basketball", "TODO"), "Sydney", "Young promising talent"));
        //     _athleteRepo.Add(new Athlete(28, "Laura Blue", "UK", 1, new Category(1, "Football", "TODO"), "London", "Olympic gold medalist"));
        // }
        //
        // if (_articleRepo.GetAll().Count() == 0)
        // {
        //     _articleRepo.Add(new Article("Sports and Health", "This article discusses the importance of sports for health.", 1, "path/to/image1.jpg", new Category(1, "Health", "TODO")));
        //     _articleRepo.Add(new Article("Tech Innovations", "Innovations in technology that shape the future.", 2, "path/to/image2.jpg", new Category(2, "Tech", "TODO")));
        //     _articleRepo.Add(new Article("The Best Athletes", "Exploring the top athletes of 2023.", 3, "path/to/image3.jpg", new Category(3, "Society", "TODO")));
        //     _articleRepo.Add(new Article("Fitness Tips", "How to stay fit and healthy throughout the year.", 1, "path/to/image4.jpg", new Category(1, "Health", "TODO")));
        // }
        
        // var athletes = _athleteRepo.GetAll();
        // var articles = _articleRepo.GetAll();
        //
        // var sortedAthletesByAge = _athleteRepo.GetAthletesSortedByAge();
        // var sortedArticlesById = _articleRepo.GetArticlesSortedByIdDescending();
        //
        // ViewData["SortedAthletesByAge"] = sortedAthletesByAge;
        // ViewData["SortedArticlesById"] = sortedArticlesById;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
