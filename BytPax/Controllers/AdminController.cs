using Microsoft.AspNetCore.Mvc;
using BytPax.Models;
using BytPax.Repositories;
using System.Linq;

namespace BytPax.Controllers
{
    public class AdminController : Controller
    {
        private readonly AdminRepo<Athlete> _athleteRepo = new AdminRepo<Athlete>();

        public IActionResult Index()
        {
            ViewData["SortedAthletesByAge"] = _athleteRepo.GetAll().OrderBy(a => a.Age).ToList();
            return View();
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Athlete model)
        {
            if (ModelState.IsValid)
            {
                _athleteRepo.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var athlete = _athleteRepo.GetEntityById(id);
            if (athlete == null) return NotFound();
            return View(athlete);
        }

        [HttpPost]
        public IActionResult Edit(Athlete model)
        {
            if (ModelState.IsValid)
            {
                _athleteRepo.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _athleteRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
