namespace amazon_clone.Models.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public DateTime OrderDateTime { get; set; }

        public int ShippingDetailsID { get; set; }
        public ShippingDetail ShippingDetails { get; set; } = null!;

        public int StatusID { get; set; }
        public OrderStatus OrderStatus { get; set; } = null!;

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();
    }
}
