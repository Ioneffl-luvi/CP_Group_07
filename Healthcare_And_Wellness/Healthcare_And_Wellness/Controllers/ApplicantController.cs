using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_And_Wellness.Controllers
{
    public class ApplicantController : Controller
    {
        private ManagementContext _managementContext;

        public ApplicantController(ManagementContext managementContext)
        {
            _managementContext = managementContext;
        }

        private void PopulateViewBag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
        }

        public IActionResult ListJobs()
        {
            PopulateViewBag();
            List<Job> jobs = _managementContext.jobs.ToList();
            return View(jobs);
        }

        [HttpGet]
        public IActionResult AddUser(int id)
        {
            PopulateViewBag();
            return View(new Applicant() { jobID = id });
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(Applicant user, IFormFile ResumeFile)
        {
            PopulateViewBag();

            if (ResumeFile == null || ResumeFile.Length == 0)
            {
                ModelState.AddModelError("ResumeFile", "The Resume file is required.");
                return View(user);
            }

            // Validate file extension
            var allowedExtensions = new[] { ".pdf", ".doc", ".docx" };
            var extension = Path.GetExtension(ResumeFile.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("ResumeFile", "Only PDF, DOC, and DOCX files are allowed.");
                return View(user);
            }

            // Save the file
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await ResumeFile.CopyToAsync(stream);
            }

            user.ResumeFilePath = "/resumes/" + fileName;

            _managementContext.applicants.Add(user);
            await _managementContext.SaveChangesAsync();

            var job = _managementContext.jobs.Find(user.jobID);
            if (job != null)
            {
                job.statusJob = "Applied";
                _managementContext.jobs.Update(job);
                await _managementContext.SaveChangesAsync();
            }

            return RedirectToAction("ListJobs", "Applicant");
        }

    }
}
