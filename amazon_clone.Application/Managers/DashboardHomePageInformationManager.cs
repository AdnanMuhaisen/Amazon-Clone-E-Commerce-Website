using amazon_clone.Application.Interfaces;
using amazon_clone.Domain.Models;
using amazon_clone.Domain.View_Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.Application.Managers
{
    public class DashboardHomePageInformationManager : IDashboardHomePageInformationManager
    {
        private readonly IAppUnitOfWork unitOfWork;

        public DashboardHomePageInformationManager(IAppUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public AdministratorDashboardHomeViewModel CalculateHomePageData()
        {
            int NumberOfTheProductsThatAddedInTheLast30Days, NumberOfUserRegistrationsInTheLast30Daya;
            double SalesRateForTheLast30Days;

            DateTime DateTimeBefore30DayFromTheCurrentDay = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));

            // calculate the number of the products that added in the last 30 days
            var productsAddedInTheLast30Days = unitOfWork
                .ProductRepository
                .GetAllAsNoTracking(filter:
                x => x.ProductCreationDetails.CreatedAt >= DateTimeBefore30DayFromTheCurrentDay);

            NumberOfTheProductsThatAddedInTheLast30Days = (productsAddedInTheLast30Days is null)
                ? 0
                : productsAddedInTheLast30Days.Count();

            // calculate the number of user registrations for the last 30 days
            var registeredCustomersInTheLast30Days = unitOfWork
                .UsersRepository
                .GetAllAsNoTracking(filter:
                x => x.CreationDetails.CreatedAt >= DateTimeBefore30DayFromTheCurrentDay);

            NumberOfUserRegistrationsInTheLast30Daya = (registeredCustomersInTheLast30Days is null)
                ? 0
                : registeredCustomersInTheLast30Days.Count();

            // calculate the sales rate for the last 30 days

            int SumOfProductsQuantitiesThatShippedInTheLast30Days = 0;
            var ShoppingCartsForTheShippedOrdersInTheLast30Days = unitOfWork
                .OrderRepository
                .GetAllAsNoTracking(filter:
                x => x.CreationDetails.CreatedAt >= DateTimeBefore30DayFromTheCurrentDay,
                include: i => i.Include(x => x.ShoppingCart)
                )?
                .Select(x => x.ShoppingCart)
                .ToList();

            if (ShoppingCartsForTheShippedOrdersInTheLast30Days is not null)
            {
                foreach (ShoppingCart cart in ShoppingCartsForTheShippedOrdersInTheLast30Days)
                {
                    var cartProducts = unitOfWork
                        .ShoppingCartProductRepository
                        .GetAll(filter: x => x.ShoppingCartID == cart.ShoppingCartID)?
                        .ToList();

                    ArgumentNullException.ThrowIfNull(cartProducts);

                    SumOfProductsQuantitiesThatShippedInTheLast30Days += cartProducts
                        .Select(x => x.Quantity)
                        .Sum();
                }
            }
            else
            {
                SumOfProductsQuantitiesThatShippedInTheLast30Days = 0;
            }

            int SumOfQuantitiesOfAllAvailableProducts;

            var QuantitiesOfAllAvailableProducts = unitOfWork
                .ProductRepository
                .GetAllAsNoTracking()?
                .Select(x => x.Quantity);

            SumOfQuantitiesOfAllAvailableProducts = (QuantitiesOfAllAvailableProducts is null)
                ? 0
                : QuantitiesOfAllAvailableProducts.Sum();

            SalesRateForTheLast30Days =
                (SumOfProductsQuantitiesThatShippedInTheLast30Days / (double)SumOfQuantitiesOfAllAvailableProducts);

            var administratorDashboardViewModel = new AdministratorDashboardHomeViewModel(
                NumberOfTheProductsThatAddedInTheLast30Days,
                SalesRateForTheLast30Days,
                NumberOfUserRegistrationsInTheLast30Daya
                );

            return administratorDashboardViewModel;
        }
    }
}
