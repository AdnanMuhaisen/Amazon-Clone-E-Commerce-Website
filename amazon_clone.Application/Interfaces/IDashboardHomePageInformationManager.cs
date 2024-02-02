using amazon_clone.Domain.View_Models;

namespace amazon_clone.Application.Interfaces
{
    public interface IDashboardHomePageInformationManager : IScopedService
    {
        AdministratorDashboardHomeViewModel CalculateHomePageData();
    }
}