using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BytPax.Areas.Admin.Models;
using BytPax.Models;
using BytPax.Repositories;

namespace BytPax.Areas.Admin.Controllers;
[Area("Admin")]
public class AthleteController : Controller
{
    private readonly ILogger<AthleteController> _logger;
    private readonly Repository<Athlete> _repository;
    private readonly Repository<Category> _categoryRepository;
    private readonly IWebHostEnvironment _environment;

    public AthleteController(
        ILogger<AthleteController> logger,
        Repository<Athlete> repository,
        Repository<Category> categoryRepository,
        IWebHostEnvironment environment)
    {
        _logger = logger;
        _repository = repository;
        _categoryRepository = categoryRepository;
        _environment = environment;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var athletes = _repository.GetAll();
        return View(athletes);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = _categoryRepository.GetAll();
        return View(new AthleteCreateViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AthleteCreateViewModel model)
    {
        _logger.LogInformation("Create Athlete POST викликаний!");
        ViewBag.Categories = _categoryRepository.GetAll();

        // Логування отриманих даних
        _logger.LogInformation(
            "Form data received: FullName = {FullName}, Country = {Country}, City = {City}, Age = {Age}, Description = {Description}, CategoryId = {CategoryId}",
            model.FullName, model.Country, model.City, model.Age, model.Description, model.CategoryId);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Create Athlete: Модель не валідна. Errors: {Errors}",
                string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            return View(model);
        }

        var athlete = new Athlete
        {
            FullName = model.FullName,
            Country = model.Country,
            City = model.City,
            Age = model.Age,
            Description = model.Description,
            CategoryId = model.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        _logger.LogInformation("Create Athlete: Створення нового об'єкта Athlete");

        _repository.Add(athlete);

        TempData["SuccessMessage"] = "Атлета успішно створено!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var athlete = _repository.GetById(id);
        if (athlete == null) return NotFound();

        ViewBag.Categories = _categoryRepository.GetAll();

        var model = new AthleteCreateViewModel
        {
            Id = athlete.Id,
            FullName = athlete.FullName,
            Country = athlete.Country,
            City = athlete.City,
            Age = athlete.Age,
            Description = athlete.Description,
            CategoryId = athlete.CategoryId
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AthleteCreateViewModel model)
    {
        ViewBag.Categories = _categoryRepository.GetAll();

        // Логування отриманих даних
        _logger.LogInformation(
            "Form data received: FullName = {FullName}, Country = {Country}, City = {City}, Age = {Age}, Description = {Description}, CategoryId = {CategoryId}",
            model.FullName, model.Country, model.City, model.Age, model.Description, model.CategoryId);

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Edit Athlete: Модель не валідна. Errors: {Errors}",
                string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            return View(model);
        }

        var athlete = _repository.GetById(model.Id);
        if (athlete == null)
        {
            _logger.LogWarning("Edit Athlete: Не знайдено спортсмена з ID = {Id}", model.Id);
            return NotFound();
        }

        athlete.FullName = model.FullName;
        athlete.Country = model.Country;
        athlete.City = model.City;
        athlete.Age = model.Age;
        athlete.Description = model.Description;
        athlete.CategoryId = model.CategoryId;

        _repository.Update(athlete);
        TempData["SuccessMessage"] = "Дані спортсмена оновлено!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var athlete = _repository.GetById(id);
        if (athlete == null) return NotFound();

        var model = new AthleteCreateViewModel
        {
            Id = athlete.Id,
            FullName = athlete.FullName,
            Country = athlete.Country,
            City = athlete.City,
            Age = athlete.Age,
            Description = athlete.Description,
            CategoryId = athlete.CategoryId
        };

        return View(model);
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var athlete = _repository.GetById(id);
        if (athlete == null) return NotFound();

        _repository.Delete(id);
        TempData["SuccessMessage"] = "Спортсмена видалено!";
        return RedirectToAction("Index");
    }

    private int GetNextId()
    {
        var all = _repository.GetAll();
        return all.Any() ? all.Max(a => a.Id) + 1 : 1;
    }
}
