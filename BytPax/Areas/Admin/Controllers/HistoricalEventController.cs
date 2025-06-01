using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BytPax.Areas.Admin.Models;
using BytPax.Models;
using BytPax.Repositories;

namespace BytPax.Areas.Admin.Controllers;

[Area("Admin")]
public class HistoricalEventController : Controller
{
    private readonly ILogger<HistoricalEventController> _logger;
    private readonly Repository<HistoricalEvent> _repository;
    private readonly Repository<Category> _categoryRepository;
    private readonly Repository<Sport> _sportRepository;
    private readonly IWebHostEnvironment _environment;

    public HistoricalEventController(
        ILogger<HistoricalEventController> logger,
        Repository<HistoricalEvent> repository,
        Repository<Sport> sportRepository,
        Repository<Category> categoryRepository,
        IWebHostEnvironment environment)
    {
        _logger = logger;
        _repository = repository;
        _sportRepository = sportRepository;
        _environment = environment;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var events = _repository.GetAll();
        return View(events);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Sports = _sportRepository.GetAll();
        ViewBag.Categories = _categoryRepository.GetAll();
        return View(new HistoricalEventCreateViewModel());
    }

    [HttpPost]
    public IActionResult Create(HistoricalEventCreateViewModel model)
    {
        ViewBag.Sports = _sportRepository.GetAll();
        if (_sportRepository.GetById(model.SportId) == null)
        {
            ModelState.AddModelError(nameof(model.SportId), "Обраний вид спорту не існує");
        }

        ViewBag.Categories = _categoryRepository.GetAll();
        if (_categoryRepository.GetById(model.CategoryId) == null)
        {
            ModelState.AddModelError(nameof(model.CategoryId), "Обрана категорія не існує");
        }


        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Create: Модель не валідна: {Errors}",
                string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            return View(model);
        }

        string? imagePath = null;
        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);
            var fileName = Path.GetFileName(model.ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.ImageFile.CopyTo(stream);
            }

            imagePath = fileName;
        }

        var newEvent = new HistoricalEvent
        {
            Id = GetNextId(),
            Title = model.Title,
            Description = model.Description,
            EventDate = DateTime.SpecifyKind(model.EventDate, DateTimeKind.Utc),
            SportId = model.SportId,
            ImportanceLevel = model.ImportanceLevel,
            ImagePath = imagePath,
            CategoryId = model.CategoryId // <--- обов'язково передати категорію
        };

        _repository.Add(newEvent);

        TempData["SuccessMessage"] = "Історичну подію успішно додано!";
        return RedirectToAction("Index");
    }


    private int GetNextId()
    {
        var allEvents = _repository.GetAll();
        return allEvents.Any() ? allEvents.Max(e => e.Id) + 1 : 1;
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var historicalEvent = _repository.GetById(id);
        if (historicalEvent == null) return NotFound();

        ViewBag.Sports = _sportRepository.GetAll();
        ViewBag.Categories = _categoryRepository.GetAll();

        var model = new HistoricalEventCreateViewModel
        {
            Id = historicalEvent.Id,
            Title = historicalEvent.Title,
            Description = historicalEvent.Description,
            EventDate = historicalEvent.EventDate,
            SportId = historicalEvent.SportId,
            // ImportanceLevel = historicalEvent.ImportanceLevel,
            ImagePath = historicalEvent.ImagePath
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(HistoricalEventCreateViewModel model)
    {
        ViewBag.Sports = _sportRepository.GetAll();

        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Edit: Модель не валідна: {Errors}",
                string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            return View(model);
        }

        var historicalEvent = _repository.GetById(model.Id);
        if (historicalEvent == null) return NotFound();

        historicalEvent.Title = model.Title;
        historicalEvent.Description = model.Description;
        historicalEvent.EventDate = model.EventDate;
        historicalEvent.SportId = model.SportId;
        // historicalEvent.ImportanceLevel = model.ImportanceLevel;

        if (model.ImageFile != null && model.ImageFile.Length > 0)
        {
            var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);
            var fileName = Path.GetFileName(model.ImageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (!string.IsNullOrEmpty(historicalEvent.ImagePath))
            {
                var oldFile = Path.Combine(uploadsFolder, historicalEvent.ImagePath);
                if (System.IO.File.Exists(oldFile))
                {
                    System.IO.File.Delete(oldFile);
                }
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.ImageFile.CopyTo(stream);
            }

            historicalEvent.ImagePath = fileName;
        }

        _repository.Update(historicalEvent);

        TempData["SuccessMessage"] = "Історичну подію оновлено!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var historicalEvent = _repository.GetById(id);
        if (historicalEvent == null) return NotFound();

        var model = new HistoricalEventCreateViewModel
        {
            Id = historicalEvent.Id,
            Title = historicalEvent.Title,
            Description = historicalEvent.Description
        };

        return View(model);
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var historicalEvent = _repository.GetById(id);
        if (historicalEvent == null) return NotFound();

        _repository.Delete(id);

        TempData["SuccessMessage"] = "Історичну подію успішно видалено!";
        return RedirectToAction("Index");
    }
}
