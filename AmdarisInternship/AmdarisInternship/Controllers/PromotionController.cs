using AmdarisInternship.API.Dtos.PromotionDtos;
using AmdarisInternship.API.Exceptions;
using AmdarisInternship.API.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AmdarisInternship.API.Controllers
{
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
        public IActionResult Post([FromBody] PromotionWithPromotionModuleDto dto)
        {
            var promotion = _promotionService.AddNewModuleWithModuleGrading(dto);

            if (promotion == null)
            {
                return BadRequest("Promotion already exists");
            }

            return Ok();
        }

        [HttpPut("{id}")]
        [ApiExceptionFilter]
        public IActionResult Put(int id)
        {
            return Ok();
        }
    }
}
