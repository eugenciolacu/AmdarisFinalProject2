using AmdarisInternship.Domain.Auth;
using System;
using System.Collections.Generic;

namespace AmdarisInternship.Domain
{
    public class Lesson : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int LecturerId { get; set; }

        public int ModuleId { get; set; }

        public int PromotionId { get; set; }


        public User User { get; set; }

        public Module Module { get; set; }

        public List<Attachment> Attachments { get; set; }

        public List<Grade> Grades { get; set; }

        public Promotion Promotion { get; set; }
    }
}
