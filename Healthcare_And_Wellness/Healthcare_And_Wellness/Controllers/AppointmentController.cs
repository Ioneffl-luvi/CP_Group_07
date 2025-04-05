using Healthcare_And_Wellness.Data;
using Healthcare_And_Wellness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healthcare_And_Wellness.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ManagementContext _context;

        public AppointmentController(ManagementContext context)
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
            string role = HttpContext.Session.GetString("Role");
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));

            if (role == "Admin")
            {
                var allAppointments = _context.Appointments.Include(a => a.User).ToList();
                return View("Index", allAppointments);
            }
            else
            {
                var userAppointments = _context.Appointments.Where(a => a.UserId == userId).ToList();
                return View("Index", userAppointments);
            }
        }

        public IActionResult CreateAvailability()
        {
            PopulateViewBag();
            return View();
        }

        [HttpPost]
        public IActionResult CreateAvailability(Appointment appointment)
        {
            PopulateViewBag();
            if (ModelState.IsValid)
            {
                appointment.Status = "Available";
                appointment.UserId = null; // Slot is available
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        public IActionResult Book()
        {
            PopulateViewBag();
            var availableSlots = _context.Appointments.Where(a => a.Status == "Available").ToList();
            return View(availableSlots);
        }

        [HttpPost]
        public IActionResult Book(int appointmentId)
        {
            PopulateViewBag();
            var appointment = _context.Appointments.Find(appointmentId);
            if (appointment == null || appointment.Status != "Available")
            {
                return NotFound();
            }

            appointment.UserId = int.Parse(HttpContext.Session.GetString("UserId"));
            appointment.Status = "Pending";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Approve(int id)
        {
            PopulateViewBag();
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();

            appointment.Status = "Approved";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id)
        {
            PopulateViewBag();
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();

            appointment.Status = "Rejected";
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            PopulateViewBag();
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();

            return View(appointment);
        }

        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            PopulateViewBag();
            if (ModelState.IsValid)
            {
                _context.Appointments.Update(appointment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }

        public IActionResult Delete(int id)
        {
            PopulateViewBag();
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return NotFound();

            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            PopulateViewBag();
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            PopulateViewBag();
            var appointment = _context.Appointments.Include(a => a.User).FirstOrDefault(a => a.Id == id);
            if (appointment == null) return NotFound();

            return View(appointment);
        }
    }
}
