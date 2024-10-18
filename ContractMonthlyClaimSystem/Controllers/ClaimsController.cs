using ContractMonthlyClaimSystem.Data;
using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ClaimsController : Controller
    {
        private static List<Claim> Claims = new List<Claim>();

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Claim claim)
        {
            claim.ClaimId = Claims.Count + 1;
            claim.Status = ClaimStatus.Pending;
            Claims.Add(claim);
            return RedirectToAction("Submitted", new { id = claim.ClaimId });
        }

        // Track status
        public IActionResult Status(int id)
        {
            var claim = Claims.FirstOrDefault(c => c.ClaimId == id);
            return View(claim);
        }

        // Approve/Reject by Coordinator/Manager
        [HttpPost]
        public IActionResult Approve(int claimId)
        {
            var claim = Claims.FirstOrDefault(c => c.ClaimId == claimId);
            claim.Status = ClaimStatus.Approved;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reject(int claimId)
        {
            var claim = Claims.FirstOrDefault(c => c.ClaimId == claimId);
            claim.Status = ClaimStatus.Rejected;
            return RedirectToAction("Index");
        }

        public IActionResult UploadDocument(IFormFile file, int claimId)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                var claim = Claims.FirstOrDefault(c => c.ClaimId == claimId);
                claim.SupportingDocuments.Add(new Document { FilePath = filePath });
            }

            return RedirectToAction("Status", new { id = claimId });
        }

        // View all claims for coordinators/managers
        public IActionResult Index()
        {
            return View(Claims);
        }
    }
}
