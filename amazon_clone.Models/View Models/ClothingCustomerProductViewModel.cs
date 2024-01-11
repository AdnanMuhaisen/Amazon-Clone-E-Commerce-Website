using amazon_clone.Models.Models;
using System.Web.WebPages.Html;

namespace amazon_clone.Models.View_Models
{
    public class ClothingCustomerProductViewModel(ClothingProduct ClothingProduct,IEnumerable<SelectListItem> ProductSizes)
    {
        public ClothingProduct ClothingProduct { get; set; } = ClothingProduct;
        public IEnumerable<SelectListItem> ProductSizes { get; set; } = ProductSizes;
    }
}
