using AmdarisInternship.API.Dtos.ModuleWithModuleGradingTdos;
using AmdarisInternship.API.Exceptions;
using AmdarisInternship.API.Services.Interfaces;
using AmdarisInternship.Domain.Auth;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmdarisInternship.API.Controllers
{
    [Authorize (Roles = UserRoles.Administrator)]
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : Controller // ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IModuleModuleGradingsService _moduleModuleGradingsService;

        public ModuleController(IModuleModuleGradingsService moduleModuleGradingsService, IMapper mapper)
        {
            _moduleModuleGradingsService = moduleModuleGradingsService;
            _mapper = mapper;
        }

        [HttpGet]
        [ApiExceptionFilter]
        public IActionResult Get()
        {
            return Ok(_moduleModuleGradingsService.GetModulesWithModuleGradings());
        }

        [HttpGet("{id}")]
        [ApiExceptionFilter]
        public IActionResult Get(int id)
        {
            var result = _moduleModuleGradingsService.GetModuleWithModuleGradingsByModuleId(id);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        [ApiExceptionFilter]
        public IActionResult Post([FromBody] ModuleWithModuleGradingDto dto)
        {
            var moduleWithGrading = _moduleModuleGradingsService.(dto);

            if (moduleWithGrading == null)
            {
                return BadRequest("Module already exists");
            }

            return CreatedAtAction(nameof(Get), new { id = moduleWithGrading.Module.Id }, moduleWithGrading);
        }

        [HttpPut("{id}")]
        [ApiExceptionFilter]
        public IActionResult Put(int id, ModuleWithModuleGradingDto dto)
        {
            var moduleWithGrading = _moduleModuleGradingsService.UpdateModuleWithModuleGrading(id, dto);

            return CreatedAtAction(nameof(Get), new { id = moduleWithGrading.Module.Id }, moduleWithGrading);
        }
    }
}
