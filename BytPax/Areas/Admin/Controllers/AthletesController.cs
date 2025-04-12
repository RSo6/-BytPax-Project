using BytPax.Instructions;
using BytPax.Models;
using BytPax.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BytPax.Areas.Admin.Controllers
{ 
    [Area("Admin")]
    public class AthletesController : Controller
    {
        private readonly IRepository<Athlete> _repository;

        public AthletesController(IRepository<Athlete> repository)
        {
            _repository = repository;
        }

        // GET: Admin/Athletes
        public ActionResult Index()
        {
            var athletes = _repository.GetAll();
            return View(athletes);
        }

        // GET: Admin/Athletes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Athletes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Athlete model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _repository.Add(model);
            return RedirectToAction("Index");
        }

        // GET: Admin/Athletes/Edit/5
        public ActionResult Edit(long id)
        {
            var athlete = _repository.GetEntityById(id);
            if (athlete == null)
                return NotFound();

            return View(athlete);
        }

        // POST: Admin/Athletes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Athlete model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _repository.Update(model);
            return RedirectToAction("Index");
        }

        // GET: Admin/Athletes/Delete/5
        public ActionResult Delete(long id)
        {
            var athlete = _repository.GetEntityById(id);
            if (athlete == null)
                return NotFound();

            return View(athlete);
        }

        // POST: Admin/Athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
