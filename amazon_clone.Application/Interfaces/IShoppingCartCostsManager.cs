namespace amazon_clone.Application.Interfaces
{
    public interface IShoppingCartCostsManager : IScopedService
    {
        void UpdateTheTotalFeesOfTheCartOrder(decimal updatedSubTotal);
    }
}