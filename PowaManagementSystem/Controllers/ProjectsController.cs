using Microsoft.AspNetCore.Mvc;
using PowaManagementSystem.Data;
using PowaManagementSystem.Models;
using System.Linq;

namespace PowaManagementSystem.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly PowaDbContext _db;

        public ProjectsController(PowaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_db.Projects.ToList());
        }

        [HttpPost]
        public IActionResult Create(Project model)
        {
            _db.Projects.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var proj = _db.Projects.Find(id);
            if (proj == null) return NotFound();
            return View(proj);
        }

        [HttpPost]
        public IActionResult Edit(Project model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var proj = _db.Projects.Find(id);
            if (proj == null) return NotFound();
            _db.Projects.Remove(proj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}