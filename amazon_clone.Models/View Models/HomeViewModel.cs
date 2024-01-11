using amazon_clone.Models.Models;

namespace amazon_clone.Models.View_Models
{
    public class HomeViewModel
    {
        public IEnumerable<CustomerProduct> allProducts { get; set; } = Enumerable.Empty<CustomerProduct>();
        public IEnumerable<ClothingProduct> clothesProducts { get; set; } = Enumerable.Empty<ClothingProduct>();
    }
}
