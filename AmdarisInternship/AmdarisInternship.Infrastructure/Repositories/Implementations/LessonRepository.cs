using AmdarisInternship.Domain;
using AmdarisInternship.Infrastructure.Context;
using AmdarisInternship.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AmdarisInternship.Infrastructure.Repositories.Implementations
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository (AppDbContext dbContext) : base(dbContext)
        {

        }

        public IList<Lesson> GetLessonsWithAttachmentsForPromotion (int promotionId)
        {
            return _dbContext.Lessons
                .Include(x => x.Attachments)
                .Include(x => x.Promotion)
                .Include(x => x.User)
                .Where(x => x.PromotionId == promotionId)
                .OrderBy(x => x.StartTime)
                .ToList();
                 
        }

        public Lesson GetLessonWithAttachmentsByLessonId (int id)
        {
            return _dbContext.Lessons
                .Include(x => x.Attachments)
                .Include(x => x.Promotion)
                .Include(x => x.User)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
