using amazon_clone.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace amazon_clone.Models.View_Models
{
    public record PaymentViewModel(decimal OrderTotalFees,string PaymentMethod)
    {
        public decimal OrderTotalFees { get; set; } = OrderTotalFees;
        public string PaymentMethod { get; set; } = PaymentMethod;
    }
}
