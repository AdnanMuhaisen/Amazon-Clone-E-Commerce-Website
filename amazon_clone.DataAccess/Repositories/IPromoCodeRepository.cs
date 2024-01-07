using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public interface IPromoCodeRepository : IRepository<PromoCode>
    {
        void Update(PromoCode entity);
    }
}
