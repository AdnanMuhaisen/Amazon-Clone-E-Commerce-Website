using amazon_clone.Domain.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace amazon_clone.Domain.View_Models
{
    public record PaymentViewModel(decimal OrderTotalFees,string PaymentMethod)
    {
        public decimal OrderTotalFees { get; set; } = OrderTotalFees;
        public string PaymentMethod { get; set; } = PaymentMethod;
    }
}
