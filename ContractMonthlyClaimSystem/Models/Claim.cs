using System.Reflection.Metadata;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string LecturerId { get; set; }
        public string CoordinatorId { get; set; }
        public string ManagerId { get; set; }
        public double HoursWorked { get; set; }
        public double HourlyRate { get; set; }
        public string Notes { get; set; }
        public string DocumentPath { get; set; }
        public string Status { get; set; } = "Pending";
        public double TotalAmount { get; set; }
        public List<Document> SupportingDocuments { get; set; }

    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
