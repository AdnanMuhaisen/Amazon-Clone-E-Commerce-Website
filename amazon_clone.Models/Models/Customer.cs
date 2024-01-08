using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace amazon_clone.Models.Models
{
    public class Customer : Person
    {
        public int? CustomerID { get; set; }
    }
}
