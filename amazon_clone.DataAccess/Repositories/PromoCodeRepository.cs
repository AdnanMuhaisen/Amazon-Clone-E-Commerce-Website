using amazon_clone.DataAccess.Data;
using amazon_clone.Models.Models;

namespace amazon_clone.DataAccess.Repositories
{
    public class PromoCodeRepository : Repository<PromoCode>, IPromoCodeRepository
    {
        public PromoCodeRepository(AppDbContext context):base(context) { }
        
        public void Update(PromoCode entity)
        {
            _context.Update(entity);
        }
    }
}
