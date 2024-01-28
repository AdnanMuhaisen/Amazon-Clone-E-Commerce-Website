using amazon_clone.Domain.Models;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {
        void Update(PromoCode entity);
    }
}
