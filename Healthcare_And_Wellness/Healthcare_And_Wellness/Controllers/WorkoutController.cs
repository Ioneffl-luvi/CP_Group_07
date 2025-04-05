using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly ManagementContext _context;

        public WorkoutController(ManagementContext context)
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
            var workouts = _context.WorkoutPlans.ToList();
            return View(workouts);
        }

        public IActionResult Create()
        {
            PopulateViewBag();
            if (ViewBag.Role != "Admin") return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult Create(WorkoutPlan workout)
        {
            PopulateViewBag();
            if (ViewBag.Role != "Admin") return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                _context.WorkoutPlans.Add(workout);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workout);
        }

        public IActionResult Edit(int id)
        {
            PopulateViewBag();
            if (ViewBag.Role != "Admin") return RedirectToAction("Index");

            var workout = _context.WorkoutPlans.Find(id);
            if (workout == null) return NotFound();

            return View(workout);
        }

        [HttpPost]
        public IActionResult Edit(WorkoutPlan workout)
        {
            PopulateViewBag();
            if (ViewBag.Role != "Admin") return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                _context.WorkoutPlans.Update(workout);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workout);
        }

        public IActionResult Delete(int id)
        {
            PopulateViewBag();
            if (ViewBag.Role != "Admin") return RedirectToAction("Index");

            var workout = _context.WorkoutPlans.Find(id);
            if (workout == null) return NotFound();
            return View(workout);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            PopulateViewBag();
            if (ViewBag.Role != "Admin") return RedirectToAction("Index");

            var workout = _context.WorkoutPlans.Find(id);
            if (workout != null)
            {
                _context.WorkoutPlans.Remove(workout);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult StartWorkout(int id)
        {
            PopulateViewBag();
            var workout = _context.WorkoutPlans.Find(id);
            if (workout == null) return NotFound();
            return View(workout);
        }
    }
}
