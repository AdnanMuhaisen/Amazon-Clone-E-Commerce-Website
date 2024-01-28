using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace amazon_clone.Dashboard.Controllers
{
    public class AdministratorTransactionsController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AdministratorTransactionsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //error
            var allTransactions = new List<int>(); 
                //unitOfWork
            //    .AdministratorsTransactionsRepository
            //    .GetAllAsNoTracking()?
            //    .Select(x => new AdministratorTransaction
            //    {
            //        TransactionID = x.TransactionID,
            //        TransactionLog = x.TransactionLog,
            //        AdministratorID = x.AdministratorID ?? "Unknown",
            //        TransactionDateTime = x.TransactionDateTime
            //    })
            //    .ToList();

            ArgumentNullException.ThrowIfNull(allTransactions);

            return View(allTransactions);
        }
    }
}
