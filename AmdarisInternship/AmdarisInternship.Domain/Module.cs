using System;
using System.Collections.Generic;
using System.Text;

namespace AmdarisInternship.Domain
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }

        public List<Exam> Exams { get; set; }

        public List<Lesson> Lessons { get; set; }

        public List<ModuleGrading> ModuleGradings { get; set; }
    }
}
