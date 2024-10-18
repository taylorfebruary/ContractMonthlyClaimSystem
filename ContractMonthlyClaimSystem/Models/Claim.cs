using System.Reflection.Metadata;

namespace ContractMonthlyClaimSystem.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public string LecturerId { get; set; }
        public string CoordinatorId { get; set; }
        public string ManagerId { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public string Notes { get; set; }
        public ClaimStatus Status { get; set; }
        public List<Document> SupportingDocuments { get; set; }

        public decimal TotalAmount => HoursWorked * HourlyRate;
    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
