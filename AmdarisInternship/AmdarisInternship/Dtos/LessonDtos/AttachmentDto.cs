using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmdarisInternship.API.Dtos.LessonDtos
{
    public class AttachmentDto
    {
        public int Id { get; set; }

        public string AttachmentExtension { get; set; }

        public string AttachmentName { get; set; }

        public byte[] Attachment_ { get; set; }

        public int LessonId { get; set; }
    }
}
