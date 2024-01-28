using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Domain.Models
{
    public class AdministratorTransaction
    {
        [Key]
        public int TransactionID { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string TransactionLog { get; set; } = null!;
        public string? AdditionalNotes { get; set; } = null!;

        // relationships
        public string AdministratorID { get; set; } = null!;
        public Administrator Administrator { get; set; } = null!;
    }
}
