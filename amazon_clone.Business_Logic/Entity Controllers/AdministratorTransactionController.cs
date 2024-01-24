using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;

namespace amazon_clone.Dashboard.Controllers
{
    public class AdministratorTransactionController : IAdministratorTransactionController
    {
        public void AddNewAdministratorTransaction(
            IUnitOfWork unitOfWork,
            string AdministratorID,
            string TransactionLog,
            DateTime TransactionDateTime = default)
        {
            var adminTransaction = new AdministratorTransaction
            {
                AdministratorID = AdministratorID,
                TransactionLog = TransactionLog,
                TransactionDateTime = (TransactionDateTime == default) ? DateTime.Now : TransactionDateTime
            };

            unitOfWork.AdministratorsTransactionsRepository.Add(adminTransaction);
        }
    }
}
