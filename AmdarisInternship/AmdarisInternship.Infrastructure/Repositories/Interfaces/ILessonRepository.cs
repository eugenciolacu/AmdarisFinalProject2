using AmdarisInternship.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmdarisInternship.Infrastructure.Repositories.Interfaces
{
    public interface ILessonRepository : IRepository<Lesson>
    {
        IList<Lesson> GetLessonsWithAttachmentsForPromotion(int promotionId);
    }
}
