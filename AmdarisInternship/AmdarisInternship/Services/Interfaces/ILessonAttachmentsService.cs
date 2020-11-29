

using AmdarisInternship.API.Dtos.LessonDtos;

namespace AmdarisInternship.API.Services.Interfaces
{
    public interface ILessonAttachmentsService
    {
        LessonWithAttachmentsDto AddLessonWithAttachments(LessonWithAttachmentsDto dto);
    }
}
