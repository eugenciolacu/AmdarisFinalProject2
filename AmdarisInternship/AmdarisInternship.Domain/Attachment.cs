namespace AmdarisInternship.Domain
{
    public class Attachment : BaseEntity
    {
        public string AttachmentExtension { get; set; }

        public string AttachmentName { get; set; }

        public byte[] Attachment_ { get; set; }

        public int LessonId { get; set; }


        public Lesson Lesson { get; set; }
    }
}
