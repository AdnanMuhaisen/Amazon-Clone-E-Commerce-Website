using amazon_clone.Models.Models;

namespace amazon_clone.Models.View_Models
{
    public record ShippingDetailsViewModel(int OrderID,ShippingDetail shippingDetail)
    {
        public int OrderID { get; set; } = OrderID;
        public ShippingDetail ShippingDetail { get; set; } = shippingDetail;
    }
}
