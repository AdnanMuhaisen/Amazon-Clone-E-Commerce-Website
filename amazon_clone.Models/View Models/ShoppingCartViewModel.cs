using amazon_clone.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace amazon_clone.Models.View_Models
{
    public record ShoppingCartViewModel(Order order,string PromoCodeToApply)
    {

        [ValidateNever]
        public Order Order { get; set; } = order;

        [MaxLength(8)]
        public string PromoCodeToApply { get; set; } = PromoCodeToApply;
    }
}
