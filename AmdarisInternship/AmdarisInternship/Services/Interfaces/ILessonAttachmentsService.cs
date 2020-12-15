

using AmdarisInternship.API.Dtos.LessonDtos;
using System.Collections.Generic;

namespace AmdarisInternship.API.Services.Interfaces
{
    public interface ILessonAttachmentsService
    {
        LessonWithAttachmentsDto AddLessonWithAttachments(LessonWithAttachmentsDto dto);
        IList<LessonWithAttachmentsDto> GetLessonsWithAttachments(int promotionId);
        LessonWithAttachmentsDto GetLessonWithAttachmentsByLessonId(int id);
    }
}
