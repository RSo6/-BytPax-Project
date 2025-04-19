using Microsoft.AspNetCore.Mvc;
using BytPax.Models;
using BytPax.Repositories;

namespace BytPax.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AthleteController : Controller
    {
        private readonly Repository<Athlete> _repository;

        public AthleteController(Repository<Athlete> repository)
        {
            _repository = repository;
        }

        // GET: /Admin/Athlete
        public IActionResult Index()
        {
            var athletes = _repository.GetAll();
            return View(athletes);
        }

        // GET: /Admin/Athlete/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Athlete/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(athlete);
                return RedirectToAction(nameof(Index));
            }
            return View(athlete);
        }

        // GET: /Admin/Athlete/Edit/5
        public IActionResult Edit(int id)
        {
            var athlete = _repository.GetById(id);
            if (athlete == null) return NotFound();
            return View(athlete);
        }

        // POST: /Admin/Athlete/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Athlete athlete)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(athlete);
                return RedirectToAction(nameof(Index));
            }
            return View(athlete);
        }

        // GET: /Admin/Athlete/Delete/5
        public IActionResult Delete(int id)
        {
            var athlete = _repository.GetById(id);
            if (athlete == null) return NotFound();
            return View(athlete);
        }

        // POST: /Admin/Athlete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Athlete/Details/5
        public IActionResult Details(int id)
        {
            var athlete = _repository.GetById(id);
            if (athlete == null) return NotFound();
            return View(athlete);
        }
    }
}
