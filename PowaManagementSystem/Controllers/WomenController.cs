using Microsoft.AspNetCore.Mvc;
using PowaManagementSystem.Data;
using PowaManagementSystem.Models;
using System.Linq;

namespace PowaManagementSystem.Controllers
{
    public class WomenController : Controller
    {
        private readonly PowaDbContext _db;

        public WomenController(PowaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_db.Women.ToList());
        }

        [HttpPost]
        public IActionResult Create(Woman model)
        {
            _db.Women.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var woman = _db.Women.Find(id);
            if (woman == null) return NotFound();
            return View(woman);
        }

        [HttpPost]
        public IActionResult Edit(Woman model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var woman = _db.Women.Find(id);
            if (woman == null) return NotFound();
            _db.Women.Remove(woman);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}