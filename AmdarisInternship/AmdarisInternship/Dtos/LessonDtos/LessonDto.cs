using System;

namespace AmdarisInternship.API.Dtos.LessonDtos
{
    public class LessonDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int LecturerId { get; set; }

        public int ModuleId { get; set; }

        public int PromotionId { get; set; }
    }
}
