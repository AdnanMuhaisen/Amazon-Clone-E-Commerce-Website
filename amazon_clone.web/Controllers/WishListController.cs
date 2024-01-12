using amazon_clone.DataAccess.Repositories;
using amazon_clone.Models.Models;
using amazon_clone.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace amazon_clone.web.Controllers
{
    public class WishListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WishListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string UserID)
        {
            //possibly null reference exception
            var wishList = _unitOfWork
                 .WishListRepository
                 .GetAsNoTracking(filter: x => x.CustomerID == UserID,
                 include: i => i.Include(x => x.Products!)
                 .ThenInclude(y => y.Category));

            ArgumentNullException.ThrowIfNull(nameof(wishList));

            return View(wishList);
        }
    }
}
