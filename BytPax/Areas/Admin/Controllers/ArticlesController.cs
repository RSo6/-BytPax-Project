using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BytPax.Areas.Admin.Models;
using BytPax.Models;
using BytPax.Repositories;

namespace BytPax.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly Repository<Article> _repository;
        private readonly Repository<Category> _categoryRepository;
        private readonly IWebHostEnvironment _environment;

        public ArticleController(
            ILogger<ArticleController> logger,
            Repository<Article> repository,
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
            var articles = _repository.GetAll();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _categoryRepository.GetAll();
            ViewBag.Categories = categories;
            return View(new ArticleCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(ArticleCreateViewModel model)
        {
            var categories = _categoryRepository.GetAll();
            ViewBag.Categories = categories;

            // Валідовуємо модель перед обробкою
            if (!ModelState.IsValid)
            {
                _logger.LogInformation(
                    "Form data received: Topic = {Topic}, BodyText = {BodyText}, CategoryId = {CategoryId}, ImageFile = {ImageFile}",
                    model.Topic, model.BodyText, model.CategoryId, model.ImageFile?.FileName);

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogWarning(error.ErrorMessage);
                }

                return View(model);  // Повертаємо форму з помилками
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

            var newArticle = new Article
            {
                Id = GetNextId(),
                Topic = model.Topic,
                BodyText = model.BodyText,
                CategoryId = model.CategoryId,
                ImagePath = imagePath,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _repository.Add(newArticle);

            TempData["SuccessMessage"] = "Статтю успішно додано!";
            return RedirectToAction("Index");
        }

        private int GetNextId()
        {
            var allArticles = _repository.GetAll();
            return allArticles.Any() ? allArticles.Max(a => a.Id) + 1 : 1;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = _repository.GetById(id);
            if (article == null) return NotFound();

            var categories = _categoryRepository.GetAll();
            ViewBag.Categories = categories;

            var model = new ArticleCreateViewModel
            {
                Id = article.Id,
                Topic = article.Topic,
                BodyText = article.BodyText,
                CategoryId = article.CategoryId,
                ImagePath = article.ImagePath
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleCreateViewModel model)
        {
            ViewBag.Categories = _categoryRepository.GetAll();

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Edit: Модель не валідна: {Errors}",
                    string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                return View(model);  // Повертаємо користувачу з помилками
            }

            var article = _repository.GetById(model.Id);
            if (article == null)
            {
                _logger.LogWarning("Edit: Статтю з ID = {Id} не знайдено", model.Id);
                return NotFound();
            }

            article.Topic = model.Topic;
            article.BodyText = model.BodyText;
            article.CategoryId = model.CategoryId;
            article.UpdatedAt = DateTime.UtcNow;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Path.GetFileName(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                if (!string.IsNullOrEmpty(article.ImagePath))
                {
                    var oldFile = Path.Combine(uploadsFolder, article.ImagePath);
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                        _logger.LogInformation("Edit: Старе зображення видалено: {File}", oldFile);
                    }
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }

                article.ImagePath = fileName;
            }

            _repository.Update(article);
            _logger.LogInformation("Edit: Статтю з ID = {Id} оновлено", article.Id);

            TempData["SuccessMessage"] = "Статтю оновлено!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var article = _repository.GetById(id);
            if (article == null) return NotFound();

            var model = new ArticleCreateViewModel
            {
                Id = article.Id,
                Topic = article.Topic,
                BodyText = article.BodyText
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var article = _repository.GetById(id);
            if (article == null) return NotFound();

            _repository.Delete(id);

            TempData["SuccessMessage"] = "Статтю успішно видалено!";
            return RedirectToAction("Index");
        }
    }
}