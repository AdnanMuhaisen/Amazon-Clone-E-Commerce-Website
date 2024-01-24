using amazon_clone.DataAccess.Repositories;

namespace amazon_clone.Dashboard.Controllers
{
    public interface IAdministratorTransactionController
    {
        void AddNewAdministratorTransaction(
            IUnitOfWork unitOfWork,
            string AdministratorID,
            string TransactionLog,
            DateTime TransactionDateTime = default
            );
    }
}