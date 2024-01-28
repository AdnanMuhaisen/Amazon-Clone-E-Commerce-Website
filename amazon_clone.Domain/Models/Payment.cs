using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Domain.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }

        [Required, MaxLength(20)]
        public string PaymentMethod { get; set; } = null!;

        [Required]
        public DateTime PaymentDateTime { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        //relationships
        public string CustomerID { get; set; } = null!;
        public CustomerApplicationUser Customer { get; set; } = null!;

        public int OrderID { get; set; }
        public Order Order { get; set; } = null!;
    }
}
