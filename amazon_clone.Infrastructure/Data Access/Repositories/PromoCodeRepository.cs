using amazon_clone.Domain.Models;
using amazon_clone.Infrastructure.DataAccess.Data.Contexts;

namespace amazon_clone.Infrastructure.DataAccess.Repositories
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
