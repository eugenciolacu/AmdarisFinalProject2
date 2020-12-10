using System.Collections.Generic;
using AmdarisInternship.API.Dtos.UserDtos;

namespace AmdarisInternship.API.Dtos.LessonDtos
{
    public class LessonWithAttachmentsDto
    {
        public LessonDto Lesson { get; set; }
        public List<AttachmentDto> Attachments { get; set; }
        public UserDto User { get; set; }

        public LessonWithAttachmentsDto()
        {
            Attachments = new List<AttachmentDto>();
        }
    }
}
