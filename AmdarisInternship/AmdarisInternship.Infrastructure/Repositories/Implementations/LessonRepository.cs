using AmdarisInternship.Domain;
using AmdarisInternship.Infrastructure.Context;
using AmdarisInternship.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmdarisInternship.Infrastructure.Repositories.Implementations
{
    public class LessonRepository : Repository<Lesson>, ILessonRepository
    {
        public LessonRepository (AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
