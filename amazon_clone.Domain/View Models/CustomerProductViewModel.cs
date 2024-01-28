using amazon_clone.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace amazon_clone.Domain.View_Models
{
    public record CustomerProductViewModel(IEnumerable<SelectListItem> categories, CustomerProduct CustomerProduct)
    {
        [ValidateNever]
        public IEnumerable<SelectListItem> categories { get; set; } = categories;
        public CustomerProduct CustomerProduct { get; set; } = CustomerProduct;
    }
}
