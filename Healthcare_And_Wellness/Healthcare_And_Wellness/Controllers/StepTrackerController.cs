using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class StepTrackerController : Controller
    {
        private readonly ManagementContext _context;

        public StepTrackerController(ManagementContext context)
        {
            _context = context;
        }

        private void PopulateViewBag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
        }

        public IActionResult Index()
        {
            PopulateViewBag();
            string userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(userIdStr);
            var logs = _context.StepLogs
                .Where(log => log.UserId == userId)
                .OrderByDescending(log => log.Date)
                .ToList();

            return View(logs);
        }

        public IActionResult LogSteps()
        {
            PopulateViewBag();
            return View();
        }

        [HttpPost]
        public IActionResult LogSteps(StepLog log)
        {
            PopulateViewBag();
            string userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                TempData["ErrorMessage"] = "Session expired. Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(userIdStr);
            log.UserId = userId;

            log.CalculateCalories();

            if (ModelState.IsValid)
            {
                _context.StepLogs.Add(log);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(log);
        }

        public IActionResult Delete(int id)
        {
            PopulateViewBag();
            var log = _context.StepLogs.Find(id);
            if (log == null) return NotFound();

            return View(log);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            PopulateViewBag();
            var log = _context.StepLogs.Find(id);
            if (log != null)
            {
                _context.StepLogs.Remove(log);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
