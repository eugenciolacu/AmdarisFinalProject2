using AmdarisInternship.Domain.Auth;
using System;
using System.Collections.Generic;

namespace AmdarisInternship.Domain
{
    public class Exam : BaseEntity
    {
        public Nullable<float> Grade { get; set; }

        public int UserId { get; set; }

        public int ModuleId { get; set; }


        public User User { get; set; }

        public Module Module { get; set; }

        public List<ExamGradeComponent> ExamGradeComponents { get; set; }
    }
}
