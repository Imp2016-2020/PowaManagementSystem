using Microsoft.AspNetCore.Mvc;
using PowaManagementSystem.Data;
using PowaManagementSystem.Models;
using System.Linq;

namespace PowaManagementSystem.Controllers
{
    public class BoardMembersController : Controller
    {
        private readonly PowaDbContext _db;

        public BoardMembersController(PowaDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login", "Account");
            }
            return View(_db.BoardMembers.ToList());
        }

        [HttpPost]
        public IActionResult Create(BoardMember model)
        {
            _db.BoardMembers.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var bm = _db.BoardMembers.Find(id);
            if (bm == null) return NotFound();
            return View(bm);
        }

        [HttpPost]
        public IActionResult Edit(BoardMember model)
        {
            _db.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var bm = _db.BoardMembers.Find(id);
            if (bm == null) return NotFound();
            _db.BoardMembers.Remove(bm);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}