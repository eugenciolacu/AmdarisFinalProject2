using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Dtos.LessonDtos
{
    public class LessonWithAttachmentsDto
    {
        public LessonDto Lesson { get; set; }
        public List<AttachmentDto> Attachments { get; set; }

        public LessonWithAttachmentsDto()
        {
            Attachments = new List<AttachmentDto>();
        }
    }
}
