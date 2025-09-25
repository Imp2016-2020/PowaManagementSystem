using Microsoft.AspNetCore.Mvc;
using PowaManagementSystem.Data;
using PowaManagementSystem.Models;
using System.Linq;

namespace PowaManagementSystem.Controllers
{
    public class PartnersController : Controller
    {
        private readonly PowaDbContext _db;

        public PartnersController(PowaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_db.Partners.ToList());
        }

        [HttpPost]
        public IActionResult Create(Partner model)
        {
            _db.Partners.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var partner = _db.Partners.Find(id);
            if (partner == null) return NotFound();
            return View(partner);
        }

        [HttpPost]
        public IActionResult Edit(Partner model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var partner = _db.Partners.Find(id);
            if (partner == null) return NotFound();
            _db.Partners.Remove(partner);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}