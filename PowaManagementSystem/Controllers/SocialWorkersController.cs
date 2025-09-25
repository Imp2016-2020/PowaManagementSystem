using Microsoft.AspNetCore.Mvc;
using PowaManagementSystem.Data;
using PowaManagementSystem.Models;
using System.Linq;

namespace PowaManagementSystem.Controllers
{
    public class SocialWorkersController : Controller
    {
        private readonly PowaDbContext _db;

        public SocialWorkersController(PowaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_db.SocialWorkers.ToList());
        }

        [HttpPost]
        public IActionResult Create(SocialWorker model)
        {
            _db.SocialWorkers.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var sw = _db.SocialWorkers.Find(id);
            if (sw == null) return NotFound();
            return View(sw);
        }

        [HttpPost]
        public IActionResult Edit(SocialWorker model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var sw = _db.SocialWorkers.Find(id);
            if (sw == null) return NotFound();
            _db.SocialWorkers.Remove(sw);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}