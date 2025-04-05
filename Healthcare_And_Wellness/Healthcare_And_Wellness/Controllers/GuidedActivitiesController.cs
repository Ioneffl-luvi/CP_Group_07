using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class GuidedActivitiesController : Controller
    {
        private readonly ManagementContext _context;

        public GuidedActivitiesController(ManagementContext context)
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
            return View(_context.GuidedActivities.OrderByDescending(a => a.CreatedAt).ToList());
        }

        public IActionResult Details(int id)
        {
            PopulateViewBag();
            var activity = _context.GuidedActivities.Find(id);
            if (activity == null) return NotFound();
            return View(activity);
        }

        public IActionResult Create()
        {
            PopulateViewBag();
            if (HttpContext.Session.GetString("Role") != "Admin") return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult Create(GuidedActivity activity)
        {
            PopulateViewBag();
            if (ModelState.IsValid)
            {
                _context.GuidedActivities.Add(activity);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        public IActionResult Edit(int id)
        {
            PopulateViewBag();
            if (HttpContext.Session.GetString("Role") != "Admin") return RedirectToAction("Index");
            var activity = _context.GuidedActivities.Find(id);
            if (activity == null) return NotFound();
            return View(activity);
        }

        [HttpPost]
        public IActionResult Edit(GuidedActivity activity)
        {
            PopulateViewBag();
            if (ModelState.IsValid)
            {
                _context.GuidedActivities.Update(activity);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        public IActionResult Delete(int id)
        {
            PopulateViewBag();
            if (HttpContext.Session.GetString("Role") != "Admin") return RedirectToAction("Index");
            var activity = _context.GuidedActivities.Find(id);
            if (activity == null) return NotFound();
            return View(activity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            PopulateViewBag();
            var activity = _context.GuidedActivities.Find(id);
            if (activity == null) return NotFound();
            _context.GuidedActivities.Remove(activity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
