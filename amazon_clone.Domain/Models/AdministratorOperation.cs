using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Domain.Models
{
    public class AdministratorOperation
    {
        [Key]
        public int OperationID { get; set; }
        public DateTime OperationDateTime { get; set; }
        public string OperationLog { get; set; } = null!;
        public string? AdditionalNotes { get; set; } = null!;

        // relationships
        public string AdministratorID { get; set; } = null!;
        public Administrator Administrator { get; set; } = null!;
    }
}
