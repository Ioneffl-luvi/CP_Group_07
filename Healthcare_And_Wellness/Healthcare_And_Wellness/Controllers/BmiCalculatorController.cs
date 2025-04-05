using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class BmiCalculatorController : Controller
    {
        private readonly ManagementContext _context;

        public BmiCalculatorController(ManagementContext context)
        {
            _context = context;
        }

        private void PopulateViewBag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
        }

        [HttpGet]
        public IActionResult Index()
        {
            PopulateViewBag();
            return View();
        }

        [HttpGet]
        public IActionResult BmiCalculation()
        {
            PopulateViewBag();
            return View(new BmiCalculator());
        }

        [HttpPost]
        public IActionResult BmiCalculation(BmiCalculator bmiCalculator, string actionType)
        {
            PopulateViewBag();
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            if (ModelState.IsValid)
            {
                bmiCalculator.BmiResult = bmiCalculator.CalculateBmi();

                if (actionType == "save")
                {
                    bmiCalculator.UserId = userId;
                    _context.BmiCalculations.Add(bmiCalculator);
                    _context.SaveChanges();

                    TempData["Message"] = "BMI saved successfully!";
                    return RedirectToAction("Index");
                }

                return View(bmiCalculator);
            }
            return View(new BmiCalculator());
        }

        [HttpGet]
        public IActionResult BmiHistory()
        {
            PopulateViewBag();
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                TempData["ErrorMessage"] = "Session expired! Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(userIdString);

            var userBmiRecords = _context.BmiCalculations
                                        .Where(b => b.UserId == userId)
                                        .OrderByDescending(b => b.DateRecorded)
                                        .ToList();

            return View(userBmiRecords);
        }

        [HttpPost]
        public IActionResult DeleteBmiRecord(int id)
        {
            PopulateViewBag();
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                TempData["ErrorMessage"] = "Session expired! Please log in again.";
                return RedirectToAction("Login", "Account");
            }

            int userId = int.Parse(userIdString);

            var bmiRecord = _context.BmiCalculations.FirstOrDefault(b => b.Id == id && b.UserId == userId);

            if (bmiRecord == null)
            {
                TempData["ErrorMessage"] = "Invalid request or record not found.";
                return RedirectToAction("BmiHistory");
            }

            _context.BmiCalculations.Remove(bmiRecord);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "BMI record deleted successfully.";
            return RedirectToAction("BmiHistory");
        }

    }
}
