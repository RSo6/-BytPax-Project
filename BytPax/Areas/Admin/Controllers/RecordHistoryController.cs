using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BytPax.Areas.Admin.Models;
using BytPax.Models;
using BytPax.Repositories;

namespace BytPax.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RecordHistoryController : Controller
    {
        private readonly ILogger<RecordHistoryController> _logger;
        private readonly Repository<RecordHistory> _repository;
        private readonly Repository<Category> _categoryRepository;
        private readonly Repository<Sport> _sportRepository;

        public RecordHistoryController(
            ILogger<RecordHistoryController> logger,
            Repository<RecordHistory> repository,
            Repository<Category> categoryRepository,
            Repository<Sport> sportRepository)
        {
            _logger = logger;
            _repository = repository;
            _categoryRepository = categoryRepository; // додано цю строчку
            _sportRepository = sportRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var records = _repository.GetAll();
            ViewBag.Categories = _categoryRepository.GetAll();
            return View(records);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Sports = _sportRepository.GetAll();
            ViewBag.Categories = _categoryRepository.GetAll();
            return View(new RecordHistoryCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(RecordHistoryCreateViewModel model)
        {
            ViewBag.Sports = _sportRepository.GetAll();
            ViewBag.Categories = _categoryRepository.GetAll();

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Create Record: Model is not valid. Errors: {Errors}",
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return View(model); 
            }

            var newRecord = new RecordHistory
            {
                Id = GetNextId(),
                SportId = model.SportId,
                AthleteName = model.AthleteName,
                RecordDescription = model.RecordDescription,
                RecordValue = model.RecordValue,
                DateAchieved = model.DateAchieved,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _repository.Add(newRecord);

            TempData["SuccessMessage"] = "Рекорд успішно додано!";
            return RedirectToAction("Index");
        }


        private int GetNextId()
        {
            var allRecords = _repository.GetAll();
            return allRecords.Any() ? allRecords.Max(r => r.Id) + 1 : 1;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var record = _repository.GetById(id);
            if (record == null) return NotFound();

            ViewBag.Sports = _sportRepository.GetAll();
            ViewBag.Categories = _categoryRepository.GetAll();

            var model = new RecordHistoryCreateViewModel
            {
                Id = record.Id,
                SportId = record.SportId,
                AthleteName = record.AthleteName,
                RecordDescription = record.RecordDescription,
                RecordValue = record.RecordValue,
                DateAchieved = record.DateAchieved
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RecordHistoryCreateViewModel model)
        {
            ViewBag.Sports = _sportRepository.GetAll();
            ViewBag.Categories = _categoryRepository.GetAll();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var record = _repository.GetById(model.Id);
            if (record == null) return NotFound();

            record.SportId = model.SportId;
            record.AthleteName = model.AthleteName;
            record.RecordDescription = model.RecordDescription;
            record.RecordValue = model.RecordValue;
            record.DateAchieved = model.DateAchieved;
            record.UpdatedAt = DateTime.UtcNow;

            _repository.Update(record);

            TempData["SuccessMessage"] = "Рекорд оновлено!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var record = _repository.GetById(id);
            if (record == null) return NotFound();

            var model = new RecordHistoryCreateViewModel
            {
                Id = record.Id,
                SportId = record.SportId,
                AthleteName = record.AthleteName,
                RecordDescription = record.RecordDescription,
                RecordValue = record.RecordValue,
                DateAchieved = record.DateAchieved
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var record = _repository.GetById(id);
            if (record == null) return NotFound();

            _repository.Delete(id);

            TempData["SuccessMessage"] = "Рекорд видалено!";
            return RedirectToAction("Index");
        }
    }
}
