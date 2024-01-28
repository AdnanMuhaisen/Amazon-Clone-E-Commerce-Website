namespace amazon_clone.Domain.Models
{
    public class OrderStatus
    {
        public int StatusID { get; set; }
        public string Status { get; set; } = null!;

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();
    }
}
