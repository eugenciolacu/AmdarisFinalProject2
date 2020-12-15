using AmdarisInternship.API.Dtos.PromotionDtos;
using AmdarisInternship.API.Exceptions;
using AmdarisInternship.API.Services.Interfaces;
using AmdarisInternship.Domain.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmdarisInternship.API.Controllers
{
    [Authorize(Roles = UserRoles.Administrator)]
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPromotionService _promotionService;

        public PromotionController(IMapper mapper, IPromotionService promotionService)
        {
            _mapper = mapper;
            _promotionService = promotionService;
        }

        [HttpGet]
        [ApiExceptionFilter]
        public IActionResult Get()
        {
            return Ok(_promotionService.GetPromotions());
        }

        [HttpGet("{id}")]
        [ApiExceptionFilter]
        public IActionResult Get(int id)
        {
            var result = _promotionService.GetPromotionById(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ApiExceptionFilter]
        public IActionResult Post([FromBody] PromotionWithPromotionModuleDto dto)
        {
            var promotion = _promotionService.AddNewPromotionWithPromotionModule(dto);

            if (promotion == null)
            {
                return BadRequest("Promotion already exists");
            }

            return Ok(promotion);
        }

        [HttpPut("{id}")]
        [ApiExceptionFilter]
        public IActionResult Put(int id, PromotionDto dto)
        {
            var promotion = _promotionService.UpdatePromotion(id, dto);

            return CreatedAtAction(nameof(Get), dto);
        }
    }
}
