using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly ManagementContext _context;

        public RecommendationController(ManagementContext context)
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

            var bmi = _context.BmiCalculations
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.DateRecorded)
                .FirstOrDefault();

            var assessment = _context.SelfAssessments
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.TakenAt)
                .FirstOrDefault();

            string recommendation = GenerateAdvice(bmi, assessment);

            // Save recommendation
            var rec = new HealthRecommendation
            {
                UserId = userId,
                RecommendationText = recommendation
            };
            _context.HealthRecommendations.Add(rec);
            _context.SaveChanges();

            return View(rec);
        }

        private string GenerateAdvice(BmiCalculator bmi, SelfAssessment assessment)
        {
            string advice = "";

            if (bmi == null)
            {
                advice += "No BMI data found. Please record your BMI.\n";
            }
            else
            {
                if (bmi.BmiResult < 18.5)
                    advice += "Your BMI indicates you're underweight. Consider a balanced diet with healthy fats and proteins.\n";
                else if (bmi.BmiResult < 24.9)
                    advice += "Your BMI is normal. Keep up with your healthy lifestyle and regular activity.\n";
                else if (bmi.BmiResult < 29.9)
                    advice += "You're in the overweight range. Try incorporating more exercise and a calorie-controlled diet.\n";
                else
                    advice += "You're in the obese range. Consult a healthcare provider for a personalized plan.\n";
            }

            if (assessment == null)
            {
                advice += "No self-assessment found. Please complete one to evaluate your mental wellness.\n";
            }
            else
            {
                if (assessment.TotalScore <= 5)
                    advice += "Your mental wellness looks good! Continue healthy practices like socializing and rest.\n";
                else if (assessment.TotalScore <= 10)
                    advice += "You may be mildly stressed. Consider relaxation exercises or short breaks.\n";
                else
                    advice += "You're experiencing high stress. Seek professional support or guided therapy sessions.\n";
            }

            return advice;
        }

        public IActionResult History()
        {
            PopulateViewBag();
            string userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Account");
            int userId = int.Parse(userIdStr);

            var history = _context.HealthRecommendations
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.GeneratedAt)
                .ToList();

            return View(history);
        }
    }
}
