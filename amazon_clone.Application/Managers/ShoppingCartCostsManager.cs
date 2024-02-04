using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Enums;
using amazon_clone.Domain.Users.CurrentUsers;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using amazon_clone.Utility.App_Details;

namespace amazon_clone.Application.Managers
{
    public class ShoppingCartCostsManager : IShoppingCartCostsManager
    {
        private readonly IAppUnitOfWork _unitOfWork;

        public ShoppingCartCostsManager(IAppUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void UpdateTheTotalFeesOfTheCartOrder(decimal updatedSubTotal)
        {
            //update the total fees of the order
            var targetOrder = _unitOfWork
                .OrderRepository
                .Get(x => x.CustomerID == CurrentCustomer.UserID
                && (x.StatusID == (int)eOrderStatuses.SHIPPED || x.StatusID == (int)eOrderStatuses.PROCESSING));

            ArgumentNullException.ThrowIfNull((targetOrder));

            targetOrder!.Total = updatedSubTotal + StaticDetails.ORDER_TAX + StaticDetails.ORDER_DELIVERY;
        }
    }
}
