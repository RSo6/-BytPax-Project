using BytPax.Instructions;
using BytPax.Models;
using BytPax.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BytPax.Areas.Admin.Controllers
{ 
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IRepository<Article> _repository;

        public ArticlesController(IRepository<Article> repository)
        {
            _repository = repository;
        }

        // GET: Admin/Articles
        public ActionResult Index()
        {
            var articles = _repository.GetAll();
            return View(articles);
        }

        // GET: Admin/Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Articles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _repository.Add(model);
            return RedirectToAction("Index");
        }

        // GET: Admin/Articles/Edit/5
        public ActionResult Edit(long id)
        {
            var article = _repository.GetEntityById(id);
            if (article == null)
                return NotFound();

            return View(article);
        }

        // POST: Admin/Articles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _repository.Update(model);
            return RedirectToAction("Index");
        }

        // GET: Admin/Articles/Delete/5
        public ActionResult Delete(long id)
        {
            var article = _repository.GetEntityById(id);
            if (article == null)
                return NotFound();

            return View(article);
        }

        // POST: Admin/Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
