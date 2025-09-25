using Microsoft.AspNetCore.Mvc;
using PowaManagementSystem.Data;
using PowaManagementSystem.Models;
using System.Linq;

namespace PowaManagementSystem.Controllers
{
    public class BranchesController : Controller
    {
        private readonly PowaDbContext _db;

        public BranchesController(PowaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_db.Branches.ToList());
        }

        [HttpPost]
        public IActionResult Create(Branch model)
        {
            _db.Branches.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var branch = _db.Branches.Find(id);
            if (branch == null) return NotFound();
            return View(branch);
        }

        [HttpPost]
        public IActionResult Edit(Branch model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var branch = _db.Branches.Find(id);
            if (branch == null) return NotFound();
            _db.Branches.Remove(branch);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}