using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Models.Models
{
    public class CreationDetails
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
