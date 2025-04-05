using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healthcare_And_Wellness.Controllers
{
     public class SupportPostsController : Controller
    {
        private readonly ManagementContext _context;

        public SupportPostsController(ManagementContext context)
        {
            _context = context;
        }

        private void PopulateViewBag()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
        }

        public IActionResult Index(string keyword)
        {
            PopulateViewBag();
            var query = _context.SupportPosts.Include(p => p.User).Include(p => p.Comments).Include(p => p.Reactions).AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.Content.Contains(keyword));
            }

            var posts = query
                .OrderByDescending(p => p.IsPinned)
                .ThenByDescending(p => p.PostedAt)
                .ToList();

            return View(posts);

        }

        public IActionResult Create()
        {
            PopulateViewBag();
            return View();
        }

        [HttpPost]
        public IActionResult Create(SupportPost post)
        {
            PopulateViewBag();
            string userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr))
                return RedirectToAction("Login", "Account");

            post.UserId = int.Parse(userIdStr);
            post.PostedAt = DateTime.Now;
            _context.SupportPosts.Add(post);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult React(int postId, string type)
        {
            PopulateViewBag();
            string userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Account");
            int userId = int.Parse(userIdStr);

            var reaction = _context.SupportReactions
                .FirstOrDefault(r => r.PostId == postId && r.UserId == userId);

            if (reaction == null)
            {
                reaction = new SupportReaction { PostId = postId, UserId = userId, ReactionType = type };
                _context.SupportReactions.Add(reaction);
            }
            else
            {
                reaction.ReactionType = type;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Comment(int postId, string content, bool isAnonymous)
        {
            PopulateViewBag();
            string userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Account");

            var comment = new SupportComment
            {
                PostId = postId,
                UserId = int.Parse(userIdStr),
                Content = content,
                CommentedAt = DateTime.Now,
                IsAnonymous = isAnonymous
            };
            _context.SupportComments.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Report(int postId, string reason)
        {
            PopulateViewBag();
            string userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Account");

            var report = new SupportReport
            {
                PostId = postId,
                UserId = int.Parse(userIdStr),
                Reason = reason,
                ReportedAt = DateTime.Now
            };
            _context.SupportReports.Add(report);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Pin(int id)
        {
            PopulateViewBag();
            var role = HttpContext.Session.GetString("Role");
            if (role != "Admin") return RedirectToAction("Index");

            var post = _context.SupportPosts.Find(id);
            if (post != null)
            {
                post.IsPinned = !post.IsPinned;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            PopulateViewBag();
            var userIdStr = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Account");
            int userId = int.Parse(userIdStr);

            var post = _context.SupportPosts.FirstOrDefault(p => p.PostId == id);
            if (post == null || (post.UserId != userId && role != "Admin"))
                return RedirectToAction("Index");

            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(SupportPost post)
        {
            PopulateViewBag();
            var userIdStr = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");
            if (string.IsNullOrEmpty(userIdStr)) return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdStr);

            var targetPost = _context.SupportPosts.FirstOrDefault(p => p.PostId == post.PostId);
            if (targetPost == null || (targetPost.UserId != userId && role != "Admin"))
                return RedirectToAction("Index");

            _context.SupportPosts.Remove(targetPost);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
