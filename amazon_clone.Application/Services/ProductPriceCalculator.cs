using amazon_clone.Domain.Models;
using amazon_clone.Utility.App_Details;

namespace amazon_clone.Application.Services
{
    public static class ProductPriceCalculator
    {
        public static decimal CalculateProductPrice(ShoppingCart cartThatContainingTheProduct,Product product)
        {
            ArgumentNullException.ThrowIfNull(cartThatContainingTheProduct);

            ArgumentNullException.ThrowIfNull(product);

            decimal price = 0m;

            price = product.Price - (product.Price * StaticDetails.PRODUCT_DICOUNT);

            if(cartThatContainingTheProduct.CartPromoCode is not null 
                && cartThatContainingTheProduct.IsPromoCodeApplied)
            {
                price -= PromoCodeService
                    .PromoCodeValueThatAffectThePriceOfProduct(price, cartThatContainingTheProduct.CartPromoCode);
            }

            price = Math.Round(price);

            price *= 100;
            // to get just 00.00 format
            price = decimal.Parse(price.ToString("C3").TrimStart('$'));

            return price;
        }
    }
}
