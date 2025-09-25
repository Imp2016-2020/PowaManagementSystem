using Microsoft.AspNetCore.Mvc;
using PowaManagementSystem.Data;
using PowaManagementSystem.Models;
using System.Linq;

namespace PowaManagementSystem.Controllers
{
    public class VolunteersController : Controller
    {
        private readonly PowaDbContext _db;

        public VolunteersController(PowaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_db.Volunteers.ToList());
        }

        [HttpPost]
        public IActionResult Create(Volunteer model)
        {
            _db.Volunteers.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var vol = _db.Volunteers.Find(id);
            if (vol == null) return NotFound();
            return View(vol);
        }

        [HttpPost]
        public IActionResult Edit(Volunteer model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var vol = _db.Volunteers.Find(id);
            if (vol == null) return NotFound();
            _db.Volunteers.Remove(vol);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}