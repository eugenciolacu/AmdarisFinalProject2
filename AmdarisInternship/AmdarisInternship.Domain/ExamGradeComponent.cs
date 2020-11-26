namespace AmdarisInternship.Domain
{
    public class ExamGradeComponent : BaseEntity
    {
        public int ModuleGradingId { get; set; }

        public float Grade { get; set; }

        public int ExamId { get; set; }

        public Exam Exam { get; set; }

        public ModuleGrading ModuleGrading { get; set; }
    }
}
