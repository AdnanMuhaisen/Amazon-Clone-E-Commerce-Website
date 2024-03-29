﻿namespace amazon_clone.Domain.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public decimal delivery { get; set; } = 0m;

        public decimal Tax { get; set; } = 10.00m;

        public decimal Total { get; set; }

        public DateTime OrderDateTime { get; set; }

        //relationships
        public int? ShippingDetailsID { get; set; }
        public ShippingDetail? ShippingDetails { get; set; } = null!;

        public int StatusID { get; set; }
        public OrderStatus OrderStatus { get; set; } = null!;

        public CreationDetails CreationDetails { get; set; } = new CreationDetails();

        public string CustomerID { get; set; } = null!;
        public CustomerApplicationUser Customer { get; set; } = null!;

        public int ShoppingCartID { get; set; }
        public ShoppingCart ShoppingCart { get; set; } = null!;

        public Payment? Payment { get; set; } = null!;
    }
}
