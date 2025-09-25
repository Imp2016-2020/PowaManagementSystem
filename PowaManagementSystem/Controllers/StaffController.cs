using Microsoft.AspNetCore.Mvc;
using PowaManagementSystem.Data;
using PowaManagementSystem.Models;
using System.Linq;

namespace PowaManagementSystem.Controllers
{
    public class StaffController : Controller
    {
        private readonly PowaDbContext _db;

        public StaffController(PowaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_db.Staffs.ToList());
        }

        [HttpPost]
        public IActionResult Create(Staff model)
        {
            _db.Staffs.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var staff = _db.Staffs.Find(id);
            if (staff == null) return NotFound();
            return View(staff);
        }

        [HttpPost]
        public IActionResult Edit(Staff model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var staff = _db.Staffs.Find(id);
            if (staff == null) return NotFound();
            _db.Staffs.Remove(staff);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}