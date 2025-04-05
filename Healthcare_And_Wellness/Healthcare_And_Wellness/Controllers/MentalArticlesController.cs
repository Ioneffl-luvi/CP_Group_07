using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace Healthcare_And_Wellness.Controllers
{
    public class MentalArticlesController : Controller
    {
        private readonly ManagementContext _context;
        private readonly IWebHostEnvironment _env;

        public MentalArticlesController(ManagementContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        private void PopulateViewBag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
        }

        public IActionResult Index()
        {
            PopulateViewBag();
            return View(_context.MentalArticles.OrderByDescending(a => a.PublishedDate).ToList());
        }

        public IActionResult ViewPdf(string fileName)
        {
            PopulateViewBag();
            var filePath = Path.Combine(_env.WebRootPath, "uploads/articles", fileName);
            var mime = "application/pdf";
            return PhysicalFile(filePath, mime);
        }

        public IActionResult Create()
        {
            PopulateViewBag();
            if (HttpContext.Session.GetString("Role") != "Admin") return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormFile pdfFile, string title)
        {
            PopulateViewBag();
            if (HttpContext.Session.GetString("Role") != "Admin") return RedirectToAction("Index");

            if (pdfFile == null || pdfFile.Length == 0 || Path.GetExtension(pdfFile.FileName) != ".pdf")
            {
                ModelState.AddModelError("pdfFile", "Please upload a valid PDF file.");
                return View();
            }

            // ✅ Create uploads/articles folder if it doesn't exist
            string uploadsDir = Path.Combine(_env.WebRootPath, "uploads/articles");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(pdfFile.FileName);
            string filePath = Path.Combine(uploadsDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                pdfFile.CopyTo(stream);
            }

            var article = new MentalArticle
            {
                Title = title,
                FileName = fileName,
                PublishedDate = DateTime.Now
            };
            _context.MentalArticles.Add(article);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ViewPage(string fileName)
        {
            PopulateViewBag();
            ViewBag.FileName = fileName;
            ViewBag.Title = "Mental Health Article";
            return View();
        }

        public IActionResult Delete(int id)
        {
            PopulateViewBag();
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Index");

            var article = _context.MentalArticles.Find(id);
            if (article == null) return NotFound();
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            PopulateViewBag();
            if (HttpContext.Session.GetString("Role") != "Admin")
                return RedirectToAction("Index");

            var article = _context.MentalArticles.Find(id);
            if (article == null) return NotFound();

            var filePath = Path.Combine(_env.WebRootPath, "uploads/articles", article.FileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            _context.MentalArticles.Remove(article);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ReadableView(string fileName)
        {
            PopulateViewBag();
            var filePath = Path.Combine(_env.WebRootPath, "uploads/articles", fileName);

            string extractedText = "";

            try
            {
                using (PdfDocument pdf = PdfDocument.Open(filePath))
                {
                    foreach (Page page in pdf.GetPages())
                    {
                        extractedText += page.Text + "\n";
                    }
                }
            }
            catch (Exception ex)
            {
                extractedText = "Unable to extract text from PDF: " + ex.Message;
            }

            ViewBag.FileName = fileName;
            ViewBag.Title = "Read Aloud View";
            ViewBag.ExtractedText = extractedText;

            return View();
        }
    }
}
