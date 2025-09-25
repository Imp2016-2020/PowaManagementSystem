using Microsoft.AspNetCore.Mvc;
using PowaManagementSystem.Data;
using PowaManagementSystem.Models;
using System.Linq;

namespace PowaManagementSystem.Controllers
{
    public class DonorsController : Controller
    {
        private readonly PowaDbContext _db;

        public DonorsController(PowaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_db.Donors.ToList());
        }

        [HttpPost]
        public IActionResult Create(Donor model)
        {
            _db.Donors.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var donor = _db.Donors.Find(id);
            if (donor == null) return NotFound();
            return View(donor);
        }

        [HttpPost]
        public IActionResult Edit(Donor model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var donor = _db.Donors.Find(id);
            if (donor == null) return NotFound();
            _db.Donors.Remove(donor);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}