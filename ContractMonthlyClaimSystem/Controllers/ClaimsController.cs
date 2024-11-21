using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClaimsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var claims = _context.Claims.ToList();
            return View(claims);
        }

        public IActionResult Submit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(ClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save the file
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = $"{Guid.NewGuid()}_{model.Document.FileName}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Document.CopyTo(fileStream);
                }

                // Save claim data
                var claim = new Claim
                {
                    LecturerId = model.LecturerName,
                    HoursWorked = model.HoursWorked,
                    HourlyRate = model.HourlyRate,
                    TotalAmount = model.TotalAmount,
                    Notes = model.Notes,
                    DocumentPath = uniqueFileName,
                    Status = "Pending"
                };

                _context.Claims.Add(claim);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Approve(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Approved";
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id)
        {
            var claim = _context.Claims.Find(id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
