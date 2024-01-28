namespace amazon_clone.Domain.View_Models
{
    public record AdministratorDashboardHomeViewModel(
        int NewProductsForTheLastMonth,
        double SalesRateForTheLastMoth,
        int UserRegistrationsForTheLastMonth
        )
    {
        public int NewProductsForTheLastMonth { get; set; } = NewProductsForTheLastMonth;
        public double SalesRateForTheLastMoth { get; set; } = SalesRateForTheLastMoth;
        public int UserRegistrationsForTheLastMonth { get; set; } = UserRegistrationsForTheLastMonth;
    }
}
