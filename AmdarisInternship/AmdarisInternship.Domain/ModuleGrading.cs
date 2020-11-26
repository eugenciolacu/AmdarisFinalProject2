using System.Collections.Generic;

namespace AmdarisInternship.Domain
{
    public class ModuleGrading : BaseEntity
    {
        public string Name { get; set; }

        public float Weight { get; set; }

        public int ModuleId { get; set; }

        public List<ExamGradeComponent> ExamGradeComponents { get; set; }
    }
}
