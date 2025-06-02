using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using BytPax.Areas.Admin.Models;
using BytPax.Models;
using BytPax.Repositories;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System;

namespace BytPax.Areas.Admin.Controllers
{
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
            ViewBag.Categories = _categoryRepository.GetAll()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            return View(new AthleteCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AthleteCreateViewModel model)
        {
            ViewBag.Categories = _categoryRepository.GetAll()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var category = _categoryRepository.GetById(model.CategoryId.Value);
            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Вказана категорія не існує");
                return View(model);
            }

            var nextId = GetNextId();

            var athlete = new Athlete(
                model.Id,
                model.FullName,
                model.Country,
                model.Age,
                category,
                model.City,
                model.Description
            );



            _repository.Add(athlete);

            TempData["SuccessMessage"] = "Атлета успішно створено!";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var athlete = _repository.GetById(id);
            if (athlete == null) return NotFound();

            ViewBag.Categories = _categoryRepository.GetAll()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

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
            ViewBag.Categories = _categoryRepository.GetAll()
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var athlete = _repository.GetById(model.Id);
            if (athlete == null) return NotFound();

            var category = _categoryRepository.GetById(model.CategoryId.Value);
            if (category == null)
            {
                ModelState.AddModelError("CategoryId", "Вказана категорія не існує");
                return View(model);
            }

            athlete.FullName = model.FullName;
            athlete.Country = model.Country;
            athlete.Age = model.Age;
            athlete.Category = category;
            athlete.City = model.City;
            athlete.Description = model.Description;

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
        [ValidateAntiForgeryToken]
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
}
