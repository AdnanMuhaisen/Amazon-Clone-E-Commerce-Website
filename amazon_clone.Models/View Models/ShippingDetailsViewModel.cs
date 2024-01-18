using amazon_clone.Models.Models;

namespace amazon_clone.Models.View_Models
{
    public record ShippingDetailsViewModel(int OrderID, Models.ShippingDetail shippingDetail)
    {
        public int OrderID { get; set; } = OrderID;
        public Models.ShippingDetail ShippingDetail { get; set; } = shippingDetail;
    }
}
