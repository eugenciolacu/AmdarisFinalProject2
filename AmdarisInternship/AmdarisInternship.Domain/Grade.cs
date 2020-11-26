using AmdarisInternship.Domain.Auth;

namespace AmdarisInternship.Domain
{
    public class Grade : BaseEntity
    {
        public float Grade_ { get; set; }

        public int UserId { get; set; }

        public int LessonId { get; set; }

        public Lesson Lesson { get; set; }

        public User User { get; set; }
    }
}
