using AmdarisInternship.API.Dtos.LessonDtos;
using AmdarisInternship.API.Services.Interfaces;
using AmdarisInternship.Domain;
using AmdarisInternship.Infrastructure.Repositories.Interfaces;
using AutoMapper;
using System.Collections.Generic;

namespace AmdarisInternship.API.Services.Implementations
{
    public class LessonAttachmentsService : ILessonAttachmentsService
    {
        private readonly ILessonRepository _lessonRepository;
        private IRepository<Attachment> _attachmentRepository;

        private readonly IMapper _mapper;

        public LessonAttachmentsService (IMapper mapper, ILessonRepository lessonRepository, IRepository<Attachment> attachmentRepository)
        {
            _mapper = mapper;
            _lessonRepository = lessonRepository;
            _attachmentRepository = attachmentRepository;
        }

        public IList<LessonWithAttachmentsDto> GetLessonsWithAttachments (int promotionId)
        {
            var lessons = _lessonRepository.GetLessonsWithAttachmentsForPromotion(promotionId);

            IList<LessonWithAttachmentsDto> result = new List<LessonWithAttachmentsDto>();

            foreach (var item in lessons)
            {
                LessonWithAttachmentsDto lessonWithAttachmentsDto = new LessonWithAttachmentsDto();
                lessonWithAttachmentsDto.Lesson = _mapper.Map<LessonDto>(item);

                foreach (var lessonsAttachment in item.Attachments)
                {
                    lessonWithAttachmentsDto.Attachments.Add(_mapper.Map<AttachmentDto>(lessonsAttachment));
                }

                result.Add(lessonWithAttachmentsDto);
            }

            return result;
        }

        public LessonWithAttachmentsDto AddLessonWithAttachments(LessonWithAttachmentsDto dto)
        {
            if (CheckIfLessonExistsInPromotion(dto.Lesson.Name, dto.Lesson.PromotionId))
            {
                return null;
            }

            Lesson lesson = _mapper.Map<Lesson>(dto.Lesson);

            _lessonRepository.Add(lesson);
            _lessonRepository.Save();

            for (int i = 0; i < dto.Attachments.Count; i++)
            {
                if (CheckIfAttachmentExists(dto.Attachments[i].AttachmentName, dto.Attachments[i].AttachmentExtension, lesson.LecturerId))
                {
                    continue;
                }

                Attachment attachment = new Attachment
                {
                    AttachmentExtension = dto.Attachments[i].AttachmentExtension,
                    AttachmentName = dto.Attachments[i].AttachmentName,
                    Attachment_ = dto.Attachments[i].Attachment_,
                    LessonId = lesson.Id
                };

                _attachmentRepository.Add(attachment);
            }

            _attachmentRepository.Save();

            return dto;
        }

        private bool CheckIfLessonExistsInPromotion (string name, int promotionId)
        {
            return _lessonRepository.Find(x => x.Name == name && x.PromotionId == promotionId) != null;
        }

        private bool CheckIfAttachmentExists(string attachmentName, string attachmentExtension, int lessonId)
        {
            return _attachmentRepository.Find(x => x.AttachmentName == attachmentName && x.AttachmentExtension == attachmentExtension && x.LessonId == lessonId) != null;
        }
    }
}
