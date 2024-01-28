using amazon_clone.Domain.Models;

namespace amazon_clone.Domain.View_Models
{
    public record ShippingDetailsViewModel(int OrderID, ShippingDetail shippingDetail)
    {
        public int OrderID { get; set; } = OrderID;
        public ShippingDetail ShippingDetail { get; set; } = shippingDetail;
    }
}
