using DinkToPdf.Contracts;
using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healthcare_And_Wellness.Controllers
{
    public class InjectionController : Controller
    {
        private readonly ManagementContext _context;
        private readonly IConverter _converter;

        public InjectionController(ManagementContext context, IConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        private void PopulateViewBag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
        }

        // Display all Injections
        public IActionResult Index()
        {
            PopulateViewBag();
            var Injections = _context.Injections.OrderByDescending(a => a.Time).ToList();
            return View(Injections);
        }

        // Admin: Create new injection
        public IActionResult Create()
        {
            PopulateViewBag();
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Index");
            return View(new Injection());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Injection injection)
        {
            PopulateViewBag();
            if (ModelState.IsValid)
            {
                _context.Injections.Add(injection);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(injection);
        }

        // Admin: Edit injection
        public IActionResult Edit(int id)
        {
            PopulateViewBag();
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Index");

            var injection = _context.Injections.Find(id);
            if (injection == null) return NotFound();
            return View(injection);
        }

        [HttpPost]
        public IActionResult Edit(Injection injection)
        {
            PopulateViewBag();
            if (ModelState.IsValid)
            {
                _context.Injections.Update(injection);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(injection);
        }

        // Admin: Delete injection
        public IActionResult Delete(int id)
        {
            PopulateViewBag();
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Index");

            var injection = _context.Injections.Find(id);
            if (injection == null) return NotFound();
            return View(injection);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            PopulateViewBag();
            var injection = _context.Injections.Find(id);
            if (injection != null)
            {
                _context.Injections.Remove(injection);
                _context.SaveChanges();
                _context.ChangeTracker.Clear();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            PopulateViewBag();
            var injection = _context.Injections.Find(id);
            if (injection == null) return NotFound();
            return View(injection);
        }

        public IActionResult Reserve(int id)
        {
            string userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            var reservation = new Reservation();
            int userId = int.Parse(userIdStr);
            reservation.UserId = userId;
            reservation.InjectionId = id;
            reservation.AddTime = DateTime.Now;

            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return RedirectToAction("ListInjection");
        }

        // Display all Injection reservations
        public IActionResult ListInjection()
        {
            PopulateViewBag();

            string userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            if (ViewBag.Role == "Member")
            {
                int userId = int.Parse(userIdStr);
                var list = _context.Reservations
                    .Include(x => x.User).Include(x => x.Injection)
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(a => a.AddTime).ToList();
                return View(list);
            }
            else
            {
                var list = _context.Reservations
                    .Include(x => x.User).Include(x => x.Injection)
                   .OrderByDescending(a => a.AddTime).ToList();
                return View(list);
            }

        }

    }
}
