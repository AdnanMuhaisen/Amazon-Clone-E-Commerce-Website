using amazon_clone.Domain.Models;
using System.Web.WebPages.Html;

namespace amazon_clone.Domain.View_Models;

public record ClothingCustomerProductViewModel(ClothingProduct ClothingProduct,IEnumerable<SelectListItem> ProductSizes)
{
    public ClothingProduct ClothingProduct { get; set; } = ClothingProduct;
    public IEnumerable<SelectListItem> ProductSizes { get; set; } = ProductSizes;
}
