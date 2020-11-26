using System;
using System.Collections.Generic;
using System.Text;

namespace AmdarisInternship.Domain
{
    public class Promotion : BaseEntity
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Name { get; set; }

        public List<Lesson> Lessons { get; set; }

        public List<UserPromotion> UserPromotions { get; set; }
    }
}
