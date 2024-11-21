using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimViewModel
    {
        public int Id { get; set; }

        [Required]
        public string LecturerName { get; set; }

        [Required, Range(1, 1000)]
        public double HoursWorked { get; set; }

        [Required, Range(50, 1000)]
        public double HourlyRate { get; set; }

        public string Notes { get; set; }

        [Required(ErrorMessage = "Please upload a document.")]
        public IFormFile Document { get; set; }

        public double TotalAmount => HoursWorked * HourlyRate; // Automated calculation
    }
}