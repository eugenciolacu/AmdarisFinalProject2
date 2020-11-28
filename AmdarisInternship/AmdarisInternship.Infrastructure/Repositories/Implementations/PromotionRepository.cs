using AmdarisInternship.Domain;
using AmdarisInternship.Infrastructure.Context;
using AmdarisInternship.Infrastructure.Repositories.Interfaces;

namespace AmdarisInternship.Infrastructure.Repositories.Implementations
{
    public class PromotionRepository : Repository<Promotion>, IPromotionRepository
    {
        public PromotionRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
