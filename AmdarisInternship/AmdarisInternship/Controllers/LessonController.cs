using AmdarisInternship.API.Dtos.LessonDtos;
using AmdarisInternship.API.Exceptions;
using AmdarisInternship.API.Services.Interfaces;
using AmdarisInternship.Domain.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmdarisInternship.API.Controllers
{
    [Authorize(Roles = UserRoles.Administrator)]
    [Route("api/[lesson]")]
    [ApiController]
    public class LessonController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILessonAttachmentsService _lessonAttachmentsService;

        public LessonController(IMapper mapper, ILessonAttachmentsService lessonAttachmentsService)
        {
            _mapper = mapper;
            _lessonAttachmentsService = lessonAttachmentsService;
        }

        [HttpGet]
        [ApiExceptionFilter]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [ApiExceptionFilter]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        [ApiExceptionFilter]
        public IActionResult Post([FromBody] LessonWithAttachmentsDto dto)
        {
            var lessonWithAttachments = _lessonAttachmentsService.AddLessonWithAttachments(dto);

            if (lessonWithAttachments == null)
            {
                return BadRequest("Lesson alreasy exists in promotion");
            }

            return CreatedAtAction(nameof(Get), new { id = lessonWithAttachments.Lesson.Id , promotionId = lessonWithAttachments.Lesson.PromotionId });
        }

        [HttpPut("{id}")]
        [ApiExceptionFilter]
        public IActionResult Put()
        {
            return Ok();
        }
    }
}
