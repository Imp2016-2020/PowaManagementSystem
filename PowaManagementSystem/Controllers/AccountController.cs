using Microsoft.AspNetCore.Mvc;
using PowaManagementSystem.Data;
using PowaManagementSystem.Helpers;
using PowaManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace PowaManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly PowaDbContext _db;

        public AccountController(PowaDbContext db)
        {
            _db = db;
        }

        public IActionResult Register()
        {
            return View(new User()); // Pass a new User object to avoid null Model
        }

        [HttpPost]
        public IActionResult Register(User model, string password_1, string password_2)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(model.Username)) errors.Add("Username is required");
            if (string.IsNullOrEmpty(model.Email)) errors.Add("Email is required");
            if (string.IsNullOrEmpty(password_1)) errors.Add("Password is required");
            if (password_1 != password_2) errors.Add("Passwords do not match");

            if (_db.Users.Any(u => u.Username == model.Username)) errors.Add("Username already exists");
            if (_db.Users.Any(u => u.Email == model.Email)) errors.Add("Email already exists");

            if (errors.Count > 0)
            {
                ViewBag.Errors = errors;
                return View(model);
            }

            model.Password = HashHelper.Md5(password_1);
            _db.Users.Add(model);
            _db.SaveChanges();

            /*HttpContext.Session.SetString("Username", model.Username);
            return RedirectToAction("Index", "Home");*/
            TempData["SuccessMessage"] = "Registration successful! Please login with your credentials.";
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(username)) errors.Add("Username is required");
            if (string.IsNullOrEmpty(password)) errors.Add("Password is required");

            if (errors.Count > 0)
            {
                ViewBag.Errors = errors;
                return View();
            }

            string hashed = HashHelper.Md5(password);
            var user = _db.Users.FirstOrDefault(u => u.Username == username && u.Password == hashed);
            if (user == null)
            {
                errors.Add("Wrong username/password");
                ViewBag.Errors = errors;
                return View();
            }

            HttpContext.Session.SetString("Username", username);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}