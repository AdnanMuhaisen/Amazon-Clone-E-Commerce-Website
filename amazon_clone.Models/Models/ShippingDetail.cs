using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Models.Models
{
    public class ShippingDetail
    {
        public int ID { get; set; }
        [Required]
        public string HomeAddress { get; set; } = null!;

        [EmailAddress]
        public string EmailAddress { get; set; } = null!;

        [Phone]
        public string ContactNumber { get; set; } = null!;

        [Required,MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required, MaxLength(50)]
        public string Country { get; set; } = null!;

        [Required]
        public int PinCode { get; set; }

        public Order Order { get; set; } = null!;

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();

    }
}
