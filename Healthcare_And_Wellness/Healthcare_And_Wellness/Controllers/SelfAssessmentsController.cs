using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class SelfAssessmentsController : Controller
    {
        private readonly ManagementContext _context;

        public SelfAssessmentsController(ManagementContext context)
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
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Account");
            int userId = int.Parse(userIdStr);

            var assessments = _context.SelfAssessments
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.TakenAt).ToList();

            return View(assessments);
        }

        public IActionResult Take()
        {
            PopulateViewBag();
            return View();
        }

        [HttpPost]
        public IActionResult Take(int q1, int q2, int q3, int q4, int q5)
        {
            PopulateViewBag();
            int total = q1 + q2 + q3 + q4 + q5;
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            var assessment = new SelfAssessment
            {
                UserId = userId,
                TotalScore = total,
                TakenAt = DateTime.Now
            };

            _context.SelfAssessments.Add(assessment);
            _context.SaveChanges();

            ViewBag.Score = total;
            return View("Result", assessment);
        }

        public IActionResult Result(int id)
        {
            PopulateViewBag();
            string userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Account");
            int userId = int.Parse(userIdStr);

            var result = _context.SelfAssessments
                .FirstOrDefault(x => x.AssessmentId == id && x.UserId == userId);

            if (result == null)
            {
                TempData["ErrorMessage"] = "Assessment not found.";
                return RedirectToAction("Index");
            }

            ViewBag.Score = result.TotalScore;
            return View(result);
        }

        public IActionResult Delete(int id)
        {
            PopulateViewBag();
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            var assessment = _context.SelfAssessments
                .FirstOrDefault(x => x.AssessmentId == id && x.UserId == userId);

            if (assessment == null)
            {
                TempData["ErrorMessage"] = "Assessment not found or access denied.";
                return RedirectToAction("Index");
            }

            return View(assessment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int assessmentId)
        {
            PopulateViewBag();
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            var assessment = _context.SelfAssessments
                .FirstOrDefault(x => x.AssessmentId == assessmentId && x.UserId == userId);

            if (assessment == null)
            {
                TempData["ErrorMessage"] = "Assessment not found or access denied.";
                return RedirectToAction("Index");
            }

            _context.SelfAssessments.Remove(assessment);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Assessment deleted successfully.";
            return RedirectToAction("Index");
        }

    }
}
